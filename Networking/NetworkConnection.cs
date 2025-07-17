// <copyright file="NetworkConnection.cs" company="UofU-CS3500">
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
//    This File contains our NetworkConneciton class, which wraps
//    the functionality of connecting to a server into a class for
//    use in the ChatClient.
// </summary>
namespace CS3500.Networking;

using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text;

/// <summary>
///   Wraps the StreamReader/Writer/TcpClient together so we
///   don't have to keep creating all three for network actions.
/// </summary>
public sealed class NetworkConnection : IDisposable
{
    /// <summary>
    ///   The connection/socket abstraction.
    /// </summary>
    private TcpClient tcpClient = new ();

    /// <summary>
    ///   Reading end of the connection.
    /// </summary>
    private StreamReader? reader = null;

    /// <summary>
    ///   Writing end of the connection.
    /// </summary>
    private StreamWriter? writer = null;

    /// <summary>
    /// A logger object for networkConnetion class.
    /// </summary>
    private ILogger logger;

    /// <summary>
    ///   Initializes a new instance of the <see cref="NetworkConnection"/> class.
    ///   <para>
    ///     Create a network connection object.
    ///   </para>
    /// </summary>
    /// <param name="tcpClient">
    ///   An already existing TcpClient.
    /// </param>
    /// <param name="logger">
    /// A logger object.
    /// </param>
    public NetworkConnection(TcpClient tcpClient, ILogger logger)
    {
        this.tcpClient = tcpClient;
        this.logger = logger;
        if (this.IsConnected)
        {
            // Only establish the reader/writer if the provided TcpClient is already connected.
            this.reader = new StreamReader(this.tcpClient.GetStream(), Encoding.UTF8);
            this.writer = new StreamWriter(this.tcpClient.GetStream(), Encoding.UTF8) { AutoFlush = true }; // AutoFlush ensures data is sent immediately
        }
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="NetworkConnection"/> class.
    ///   <para>
    ///     Create a network connection object.  The tcpClient will be unconnected at the start.
    ///   </para>
    /// </summary>
    /// <param name="logger">
    /// A logger object.
    /// </param>
    public NetworkConnection(ILogger logger)
        : this(new TcpClient(), logger)
    {
    }

    /// <summary>
    /// Gets a value indicating whether the socket is connected.
    /// </summary>
    public bool IsConnected
    {
        get
        {
            if (this.tcpClient != null && this.tcpClient.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    ///   Try to connect to the given host:port.
    /// </summary>
    /// <param name="host"> The URL or IP address, e.g., www.cs.utah.edu, or  127.0.0.1. </param>
    /// <param name="port"> The port, e.g., 11000. </param>
    public void Connect(string host, int port)
    {
        try
        {
            this.tcpClient.Connect(host, port);
            if (this.IsConnected)
            {
                this.reader = new StreamReader(this.tcpClient.GetStream(), new UTF8Encoding(false));
                this.writer = new StreamWriter(this.tcpClient.GetStream(), new UTF8Encoding(false)) { AutoFlush = true };
                this.logger.LogDebug("Client Connected Successfully");
            }
        }
        catch
        {
            this.logger.LogError($"Problem with connecting client to {host}:{port}");
            throw;
        }
    }

    /// <summary>
    ///   Send a message to the remote server.  If the <paramref name="message"/> contains
    ///   new lines, these will be treated on the receiving side as multiple messages.
    ///   This method should attach a newline to the end of the <paramref name="message"/>
    ///   (by using WriteLine).
    ///   If this operation can not be completed (e.g. because this NetworkConnection is not
    ///   connected), throw an InvalidOperationException.
    /// </summary>
    /// <param name="message"> The string of characters to send. </param>
    public void Send(string message)
    {
        try
        {
            this.writer?.WriteLine($"{message}");
            this.logger.LogDebug($"Sent Client: {message}");
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    /// <summary>
    ///   Read a message from the remote side of the connection.  The message will contain
    ///   all characters up to the first new line. See <see cref="Send"/>.
    ///   If this operation can not be completed (e.g. because this NetworkConnection is not
    ///   connected), throw an InvalidOperationException.
    /// </summary>
    /// <returns> The contents of the message. </returns>
    public string ReadLine()
    {
        if (!this.IsConnected)
        {
            throw new InvalidOperationException();
        }

        try
        {
            string? message = this.reader?.ReadLine() ?? throw new InvalidOperationException();
            logger.LogDebug($"Received Message: {message}");
            if (message == null)
            {
                throw new InvalidOperationException("Message was Null");
            }

            return message;
        }
        catch
        {
            this.logger.LogError("Error reading message");
            throw;
        }
    }

    /// <summary>
    ///   If connected, disconnect the connection and clean
    ///   up (dispose) any streams.
    /// </summary>
    public void Disconnect()
    {
        if (this.IsConnected)
        {
            this.reader?.Dispose();
            this.writer?.Dispose();
            this.tcpClient.Dispose();
            this.logger.LogInformation("Client successfully disconnected");
        }
    }

    /// <summary>
    ///   Automatically called with a using statement (see IDisposable).
    /// </summary>
    public void Dispose()
    {
        this.Disconnect();
    }
}
