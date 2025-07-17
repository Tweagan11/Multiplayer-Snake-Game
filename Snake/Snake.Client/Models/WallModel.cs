// <copyright file="WallModel.cs" company="UofU-CS3500">
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
//    This File contains our WallModel class used to represent the walls in the world.
// </summary>

using System.Security.Cryptography;
using System.Drawing;
using System.Text.Json.Serialization;
namespace Snake.Client.Models;

public class WallModel
{
    /// <summary>
    /// The Id used to distinguish wallModels.
    /// </summary>
    [JsonPropertyName("wall")]
    public int Id { get; set; }

    /// <summary>
    /// A Point2D representing the first end point of a wall.
    /// </summary>
    public Point2D ?p1 { get; set; }

    /// <summary>
    /// A Point2D representing the second end point of a wall.
    /// </summary>
    public Point2D ?p2 { get; set; }
}
