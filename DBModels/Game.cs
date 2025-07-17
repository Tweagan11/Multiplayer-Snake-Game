// <copyright file="Game.cs" company="UofU-CS3500">
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
//    This File contains a class representing a game in a games table.  Each
//    game is represented by the GameId.
// </summary>
using System;
using CS3500.PlayerModel;

namespace CS3500.GameModel;

/// <summary>
/// A class representing a game in a game table.
/// </summary>
public class Game
{
    /// <summary>
    /// Represents the GameId in the table.
    /// </summary>
    public int GameId { get; set; }

    /// <summary>
    /// Represents the time the game began.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Represents the time the game ended.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// A list of players in a given game.
    /// </summary>
    public ICollection<Player> Players = [];
}
