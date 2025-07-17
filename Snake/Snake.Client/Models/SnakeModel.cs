// <copyright file="SnakeModel.cs" company="UofU-CS3500">
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
//    This File contains our SnakeModel class used to represent snakes in the
//    world.  The equality operators have been overloaded so snake objects
//    can be compared by score.
// </summary>

using System.Drawing;
using System.Text.Json.Serialization;
namespace Snake.Client.Models;

public class SnakeModel
{
    /// <summary>
    /// The Id used to differentiate between snake objects.
    /// </summary>
    [JsonPropertyName("snake")]
    public int Id { get; set; }

    /// <summary>
    /// The name of the snake.
    /// </summary>
    public string ?name { get; set; }

    /// <summary>
    /// A list containing the body points of the snake.
    /// </summary>
    public List<Point2D> ?body { get; set; }

    /// <summary>
    /// The Point2D representing the direction of the snake.
    /// </summary>
    public Point2D ?dir { get; set; }

    /// <summary>
    /// The score of the snake, equal to the ammount of power ups eaten.
    /// </summary>
    public int score { get; set; }

    /// <summary>
    /// A bool reporting if a snake has crash into a wall or another snake.
    /// </summary>
    public bool died { get; set; }

    /// <summary>
    /// A bool reporting if the snake is currently alive.
    /// </summary>
    public bool alive { get; set; }

    /// <summary>
    /// A bool representing a snakes connection to the server.
    /// </summary>
    public bool dc {  get; set; }

    /// <summary>
    /// A bool representing if a snake has joined.
    /// </summary>
    public bool join { get; set; }

    /// <summary>
    /// Gets the string form of a snake.
    /// </summary>
    /// <returns>A string containing the snakes data.</returns>
    public override string ToString()
    {
        return $"Snake: {Id}, Name: {name}, direction {dir!.X}, {dir.Y}, score {score}, alive: {alive}";
    }

    /// <summary>
    /// The Equals overload used to easily sort a string of snakes.
    /// </summary>
    /// <param name="obj">A snakeModel object.</param>
    /// <returns></returns>
    public override bool Equals(object ?obj)
    {
        if (obj is null) return false;
        if (obj is SnakeModel other)
        {
            return this.score == other.score;
        }
        return false;
    }

    /// <summary>
    /// Overrides the hash code for the snake object.
    /// </summary>
    /// <returns>The snakes hash code based on its ID.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// The overloaded == operator.
    /// </summary>
    /// <param name="left">A snakeModel object.</param>
    /// <param name="right">A snakeModel object.</param>
    /// <returns></returns>
    public static bool operator ==(SnakeModel left, SnakeModel right)
    {
        if (left is null)
            return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// The overloaded != operator.
    /// </summary>
    /// <param name="left">A snakeModel object.</param>
    /// <param name="right">A snakeModel object.</param>
    /// <returns></returns>
    public static bool operator !=(SnakeModel left, SnakeModel right)
    {
        return !(left == right);
    }
}
