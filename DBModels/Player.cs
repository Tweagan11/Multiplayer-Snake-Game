// <copyright file="Player.cs" company="UofU-CS3500">
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
//    This File contains a class representing a player in a player table.  Each
//    player is represented by the PlayerId.
// </summary>
using System;
using CS3500.GameModel;

namespace CS3500.PlayerModel;

/// <summary>
/// A class representing a player in a players table.
/// </summary>
public class Player
{
    /// <summary>
    /// The Id used to differentiate players, it correlates to the snakeId.
    /// </summary>
    public int PlayerId { get; set; }

    /// <summary>
    /// The GameId of the game the player is currently playing in.
    /// </summary>
    public int GameId { get; set; }

    /// <summary>
    /// The name of the player snake.
    /// </summary>
    public string ?SnakeName { get; set; }

    /// <summary>
    /// The high score of the player.
    /// </summary>
    public int MaxScore { get; set; }

    /// <summary>
    /// The time a player joined a game.
    /// </summary>
    public DateTime EnterTime { get; set; }

    /// <summary>
    /// The time a player left a game.
    /// </summary>
    public DateTime LeaveTime { get; set; }

    /// <summary>
    /// The game the player is in.
    /// </summary>
    public Game ?Game { get; set; }
}
