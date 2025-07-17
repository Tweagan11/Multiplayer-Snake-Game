// <copyright file="SnakeClient.razor.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Chase Hammond
// Partner:   Teagan Smith
// Date:      12/5/24
// Course:    CS 3500, University of Utah, School of Computing
// Copyright: CS 3500 and Chase Hammond - This work may not be
//            copied for use in Academic Coursework.
//
// I, Chase, certify that I wrote this code from scratch and
// did not copy it in part or whole from another source.  All
// references used in the completion of the assignments are cited
// in my README file.
//
// File Contents
//
//    This File contains a partial class for the snake client class.
// </summary>
using Blazor.Extensions;
using CS3500.Networking;
using Microsoft.JSInterop;
using Snake.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CS3500.DBModels;
using System.Reflection;
using CS3500.PlayerModel;
using CS3500.GameModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Snake.Client.Pages
{
    partial class SnakeClient
    {
        private Game CurrentGame = new();

        private Dictionary<int, int> DBSnakesScores = new();

        private bool UpdateDBFlag;

        /// <summary>
        /// A helper method to add the given model to the world model.
        /// </summary>
        /// <param name="Model">The model to be added.</param>
        private void UpdateWorldObjects<T>(T Model, Dictionary<int, T> objectDict, int ID, bool remove=false)
        {
            if (remove)
            {
                lock (world)
                {
                    objectDict.Remove(ID);
                }
            }
            else
            {
                lock (world)
                {
                    objectDict[ID] = Model;
                }
            }
            
        }

        /// <summary>
        /// Starts the game in the database if the isn't one running, sets the 
        /// current game equal to the running game if there is.
        /// </summary>
        /// <param name="db">The database object. </param>
        private void StartGameDB()
        {
            using var db = new AppDbContext();
            // Reset the Current Game
            CurrentGame = new();

            int count = db.Games.Count();

            DBSnakesScores = new();

            Game game = db.Games.FirstOrDefault(o => o.GameId == count);
            if (game != null && game.StartTime == game.EndTime)
            {
                CurrentGame = game;
                UpdateDBFlag = false;
                if (game.Players.Count == 0)
                {
                    UpdateDBFlag = true;
                }
            }
            else
            {
                CurrentGame.StartTime = DateTime.Now;
                CurrentGame.EndTime = DateTime.Now;
                db.Games.Add(CurrentGame);
                UpdateDBFlag = true;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Ends the game in the database by changing the endtime.
        /// </summary>
        /// <param name="db">The database object. </param>
        private void StopGameDB()
        {
            if (!UpdateDBFlag)
            {
                return;
            }
            using var db = new AppDbContext();
            CurrentGame = db.Games.FirstOrDefault(o => o.GameId == CurrentGame.GameId);
            CurrentGame.EndTime = DateTime.Now;

            db.SaveChanges();
        }

        /// <summary>
        /// Runs a New thread to update the database without affecting the functionality of the game.
        /// </summary>
        /// <param name="snake">Current Snake to Check</param>
        /// <returns></returns>
        private async Task CheckSnakeStatuses(SnakeModel snake)
        {
            if (!UpdateDBFlag)
            {
                return;
            }
            if (snake.join)
            {
                HandleSnakeJoinDB(snake);
            }
            else if (snake.dc)
            {
                await DisconnectDBSnake(snake);
            }
            else
            {
                if (!DBSnakesScores.TryGetValue(snake.Id, out int dbScore))
                {
                    HandleSnakeJoinDB(snake);
                }
                else if (dbScore < snake.score)
                {
                    await UpdateSnakeScoreDB(snake);
                }
            }
        }

        /// <summary>
        /// Connect to the Database and Update the Snake Score
        /// </summary>
        /// <param name="snake"></param>
        /// <returns></returns>
        private async Task UpdateSnakeScoreDB(SnakeModel snake)
        {
            DBSnakesScores[snake.Id] = snake.score;
            var db = new AppDbContext();
            Player DBPlayer = db.Players.Include(p => p.Game).FirstOrDefault(p => p.PlayerId == snake.Id);
            DBPlayer.MaxScore = snake.score;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Connect to Database and Update the LeaveTime of the Snake
        /// </summary>
        /// <param name="snake">Snake End Time to Update</param>
        /// <returns></returns>
        private static async Task DisconnectDBSnake(SnakeModel snake)
        {
            var db = new AppDbContext();
            Player DBPlayer = db.Players.Include(p => p.Game).FirstOrDefault(p => p.PlayerId == snake.Id);
            DBPlayer.LeaveTime = DateTime.Now;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Updates both the Jointime and the Leavetime of the snake that has joined the game.
        /// </summary>
        /// <param name="snake"></param>
        /// <returns></returns>
        private void HandleSnakeJoinDB(SnakeModel snake)
        {
            DBSnakesScores[snake.Id] = snake.score;
            var db = new AppDbContext();

            DateTime joinTime = DateTime.Now;
            Player player = new Player();
            player.SnakeName = snake.name;
            player.PlayerId = snake.Id;
            player.GameId = CurrentGame.GameId;
            player.EnterTime = joinTime;
            player.LeaveTime = joinTime;
            player.MaxScore = snake.score;

            db.Players.Add(player);
            db.SaveChanges();
        }

        /// <summary>
        /// Gets a list of snake models sorted on score descending.
        /// </summary>
        /// <param name="model">The world model to get the snakes from.</param>
        /// <returns>A list of SnakeModel objects.</returns>
        private List<SnakeModel> GetSortedSnakes(WorldModel model)
        {
            List<SnakeModel> snakes = new List<SnakeModel>();
            foreach (SnakeModel snake in model.Snakes.Values)
            {
                snakes.Add(snake);
            }
            var sortedSnakes = snakes.OrderByDescending(snake => snake.score).ToList();
            return sortedSnakes;
        }

        /// <summary>
        /// Gets the color of a snake based on its ID.
        /// </summary>
        /// <param name="snakeId">The ID of the snake to color.</param>
        private string GetSnakeColor(int snakeId)
        {
            int hue = (snakeId * 45) + 27 % 360;
            string color = $"hsl({hue}, 100%, 50%)";
            return color;
        }

        /// <summary>
        /// Handles the key press.
        /// </summary>
        /// <param name="key">The key.</param>
        [JSInvokable]
        public void HandleKeyPress(string key)
        {

            if (network.IsConnected)
            {
                key = key.ToLower();
                if (prevKeyPress == key)
                {
                    return;
                }
                

                if (key == "arrowup" || key == "w")
                {
                    network.Send("{\"moving\":\"up\"}");
                    Logger.LogDebug("Sent Server Moving Up");
                }
                else if (key == "arrowdown" || key == "s")
                {
                    network.Send("{\"moving\":\"down\"}");
                    Logger.LogDebug("Sent Server Moving Down");
                }
                else if (key == "arrowleft" || key == "a")
                {
                    network.Send("{\"moving\":\"left\"}");
                    Logger.LogDebug("Sent Server Moving Left");

                }
                else if (key == "arrowright" || key == "d")
                {
                    network.Send("{\"moving\":\"right\"}");
                    Logger.LogDebug("Sent Server Moving Right");
                }
                    prevKeyPress = key;
            }
        }
    }
}
