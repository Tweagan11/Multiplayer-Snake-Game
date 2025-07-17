// <copyright file="ChatServer.cs" company="UofU-CS3500">
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
//    This File contains our ChatServer class that is used to
//    handle the connection to a client.
// </summary>

namespace CS3500.Chatting;

using System.Reflection.Metadata;
using System.Text;
using CS3500.Networking;
using Microsoft.Extensions.Logging;

/// <summary>
///   A simple ChatServer that handles clients separately and replies with a static message.
/// </summary>
public partial class ChatServer
{
    /// <summary>
    /// This HashSet holds all NetworkConnection class connections to the server, allowing
    /// the clients to send to all users connected.
    /// </summary>
    private static HashSet<NetworkConnection> connections = new ();

    /// <summary>
    /// Server stores a list of last 100 messages previously sent, stores the name and
    /// message to send to new users joining the chat.
    /// </summary>
    private static Queue<(string name, string message)> last50Messages = new();

    /// <summary>
    ///   The main program.
    /// </summary>
    /// <param name="args"> ignored. </param>
    private static void Main(string[] args)
    {
        Server.StartServer(HandleConnect, 11_000);
        Console.Read(); // don't stop the program.
    }

    /// <summary>
    ///   <pre>
    ///     When a new connection is established, enter a loop that receives from and
    ///     replies to a client.
    ///   </pre>
    /// </summary>
    ///
    private static void HandleConnect(NetworkConnection connection)
    {
        // handle all messages until disconnect.
        string name = "defaultUser";
        try
        {
            connection.Send("Welcome to the Sever! Please enter a name to continue:");
            name = connection.ReadLine();

            // Add connection and let others know.
            lock (connections)
            {
                connections.Add(connection);
                foreach (NetworkConnection c in connections)
                {
                    c.Send($"{name} has joined the chat!");
                }
            }

            // Give the user the last 50 messages sent.
            lock (last50Messages)
            {
                foreach (var (senderName, message) in last50Messages)
                {
                    connection.Send($"{senderName}: {message}");
                }
            }

            while (true)
            {
                var message = connection.ReadLine();
                HashSet<NetworkConnection> currConnections = new HashSet<NetworkConnection>();
                lock (connections)
                {
                    foreach (NetworkConnection userConnection in connections)
                    {
                        currConnections.Add(userConnection);
                    }
                }

                if (message != null)
                {
                    foreach (NetworkConnection userConnection in currConnections)
                    {
                        userConnection.Send($"{name}: {message}");
                    }

                    // Should not cause a deadlock, uses one lock outside of the other lock.
                    Update50Messages(name, message);
                }
            }
        }
        catch (Exception)
        {
            lock (connections)
            {
                foreach (NetworkConnection user in connections)
                {
                    user.Send($"{name} has left the chat.");
                }

                if (connection != null)
                {
                    connections.Remove(connection);
                    connection.Disconnect();
                }
            }
        }
    }

    /// <summary>
    /// Updates the previous 50 messages from the chat, allowing users to join and see
    /// a chat history
    /// </summary>
    /// <param name="name">The name of the previous sender.</param>
    /// <param name="message">The message the sender sent.</param>
    private static void Update50Messages(string name, string message)
    {
        lock (last50Messages)
        {
            last50Messages.Enqueue((name,message));
            if (last50Messages.Count > 50)
            {
                last50Messages.Dequeue();
            }
        }
    }
}