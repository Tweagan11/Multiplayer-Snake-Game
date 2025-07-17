// <copyright file="WorldModel.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Chase Hammond
// Partner:   Teagan Smith
// Date:      11/22/24
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
//    This File contains our WorldModel class used to represent the state of
//    the world.  It contains three dictionaries for each of the Model objects.
// </summary>
namespace Snake.Client.Models
{
    public class WorldModel
    {
        /// <summary>
        /// Contains all the SnakeModels that are in the world.
        /// </summary>
        public Dictionary<int, SnakeModel> Snakes = [];

        /// <summary>
        /// Contains all of the WallModels that are in the world.
        /// </summary>
        public Dictionary<int, WallModel> Walls = [];
        
        /// <summary>
        /// Contains all of the PowerUpModels that are in the world.
        /// </summary>
        public Dictionary<int, PowerUpModel> PowerUps = [];

        /// <summary>
        /// Stores the height of the world.
        /// </summary>
        public int Height;

        /// <summary>
        /// Stores the width of the world.
        /// </summary>
        public int Width;
    }
}
