// <copyright file="Server.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Chase Hammond
// Partner:   Teagan Smith
// Date:      11/7/24
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
//    This File contains our Server class that is used to start the
//    server and listen for clients trying to connect.
// </summary>
namespace CS3500.Networking;

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

/// <summary>
///   Represents a server task that waits for connections on a given
///   port and calls the provided delegate when a connection is made.
/// </summary>
public static class Server
{
    /// <summary>
    /// Logger object for the server class.
    /// </summary>
    private static readonly ILogger Logger;

    static Server()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Trace);
        });
        Logger = loggerFactory.CreateLogger("Server");
    }

    /// <summary>
    ///   Wait on a TcpListener for new connections. Alert the main program
    ///   via a callback (delegate) mechanism.
    /// </summary>
    /// <param name="handleConnect">
    ///   Handler for what the user wants to do when a connection is made.
    ///   This should be run asynchronously via a new thread.
    /// </param>
    /// <param name="port"> The port (e.g., 11000) to listen on. </param>
    public static void StartServer(Action<NetworkConnection> handleConnect, int port)
    {
        // Create new Listener for new clients
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        try
        {
            Logger.LogInformation("Starting Server...");
            listener.Start();
        }
        catch
        {
            Logger.LogError("Problems with Listener.");
            throw;
        }

        // When new clients are found, create a connection and start a new thread.
        while (true)
        {
            try
            {
                Logger.LogInformation("Listening for new Clients...");
                TcpClient newClient = listener.AcceptTcpClient();
                Logger.LogInformation("Client Found, connecting...");
                NetworkConnection newConnection = new (newClient, Logger);
                Task.Run(() => handleConnect(newConnection));
            }
            catch
            {
                Logger.LogError("Problem Accepting TcpClient");
                throw;
            }
        }
    }
}
