// <copyright file="Point2D.cs" company="UofU-CS3500">
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
//    This File contains our point2D class used to store the 2d points 
//    that the models use.
// </summary>
namespace Snake.Client.Models;

public class Point2D
{
    /// <summary>
    /// The x coordinate.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The y coordinate.
    /// </summary>
    public int Y { get; set; }
}
