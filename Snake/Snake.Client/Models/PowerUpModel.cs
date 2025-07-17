// <copyright file="PowerUpModel.cs" company="UofU-CS3500">
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
//    This File contains our PowerUpModel class used to represent 
//    power ups in the world.
// </summary>
using System.Text.Json.Serialization;

namespace Snake.Client.Models;

public class PowerUpModel
{
    /// <summary>
    /// The Id used to distinguish power up models.
    /// </summary>
    [JsonPropertyName("power")]
    public int Id {  get; set; }

    /// <summary>
    /// The point2D that represents the powerups location.
    /// </summary>
    public Point2D ?loc { get; set; }

    /// <summary>
    /// A bool reporting if a power up has been eaten.
    /// </summary>
    public bool died { get; set; }
}
