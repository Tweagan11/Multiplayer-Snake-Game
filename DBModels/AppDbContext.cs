// <copyright file="AppDbContext.cs" company="UofU-CS3500">
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
//    This File contains our AppDbContext class used to communicate
//    with the server.
// </summary>
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using CS3500.PlayerModel;
using CS3500.GameModel;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CS3500.DBModels;

/// <summary>
/// A child class to the DbContext class used to talk to the database.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// A Db set of the player tables.
    /// </summary>
    public DbSet<Player> Players { get; set; }

    /// <summary>
    /// A Db set of the games tables.
    /// </summary>
    public DbSet<Game> Games { get; set; }

    /// <summary>
    /// The connection string to be used to connect to the database.
    /// </summary>
    private string ConnectionString = string.Empty;

    public AppDbContext()
    {
        var builder = new ConfigurationBuilder();

        builder.AddUserSecrets<AppDbContext>();
        IConfigurationRoot configuration = builder.Build();
        var selectedSecrets = configuration.GetSection("DBSecrets");

        ConnectionString = new SqlConnectionStringBuilder()
        {
            DataSource = selectedSecrets["ServerName"],
            InitialCatalog = selectedSecrets["InitialCatalog"],
            UserID = selectedSecrets["UserID"],
            Password = selectedSecrets["ServerPassword"],
            ConnectTimeout = 15,
            Encrypt = false,
        }.ConnectionString;
    }

    /// <summary>
    /// Using the connections string to make the database connection.
    /// </summary>
    /// <param name="optionsBuilder">The optionsBuilder object.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}
