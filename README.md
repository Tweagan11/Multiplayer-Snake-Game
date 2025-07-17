```
Author:     Chase Hammond
Partner:    Teagan Smith
Start Date: 19-Nov-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  tweagan11, chammond123
Repo:       https://github.com/uofu-cs3500-20-fall2024/assignment-eight-chatting-bros_before_nodes_game
Commit Date:11/24/2024
Solution:   Chatting
Copyright:  CS 3500 and Teagan Smith, Chase Hammond - This work may not be copied for use in Academic Coursework.
```


# Overview of the Chat Functionality
This program represents a chat server and client.  The server handles multiple clients connecting and allows them to send messages back and forth.
Clients can connect and disconnect to the server using an appropriate IP address and port number.  The first message sent to the server will be
used as the clients name when chatting.

# Overview of Snake Game Functionality
As of 11/24, this project has been updated to contain a Multiplayer snake game, where you can connect to a server, as well as interact with a drawn world real time.

# Overview of Database Functionality
As of 12/05, this project has been updated to communicate with a Database, store the high scores and details of players to a database, where a web server can display all associated data to a web browser.

# Design Decisions
The design decisions we decided to implement are: 

- Textures for the wall and background objects.
- A leader board that shows the current playing snakes in descending order by score.  The leader board will resize as
  players connect and disconnect and prevents player names that are too long from clipping out of the box.
- When a snake dies it turns red.

# Time to completion notes
Most time spent on this assignment was done with pair programming. Due to Traveling and important events, we had to rarely split our time.
However, the expected and actual times represent the input of both parters.

Expected time: 14h

Time Spent Learning: 1 hr
Time Spent Manually Testing: 1 hr
Time Spent Creating: 9 hrs
Time Spent Debugging:  4 hr

Actual Time: 15h

# Comments to evaluators:

In our snake program, we noticed one bug that we could not figure out. The Connect and Disconnect work really well, but when a player is trying to leave the program by
clicking to a new tab or if they delete the tab they are on, it does not disconnect them on the server. We thoroughly researched what may be going on, and even tried altering the 
JavaScript and the Unload code to call the function, but it would not work for some reason. Spent about an hour or two just trying to debug and diagnose.

Another Bug we ran into was lining up the walls and not exposing any corners. Since the walls are just points given, we had to mathematically manipulate the given points
so that the functionality wasn't lost and that all walls connected seamlessly.

# Peer Programming:
Both parters were driven and committed to getting this assignment completed on time and at A quality. There weren't any disconnects in communication
or effort.  We both feel happy with how the pair programing proceeded.

# Consulted peers:
Chase Hammond
Teagan Smith
Jacob Erard
Dennis Panchecka

# References:
- https://chatgpt.com - No generated code was used, it was used for logic and syntax.
- https://getbootstrap.com/ - Bootstrap


# Consulted peers:
Chase Hammond
Teagan Smith
Jacob Erard

# References:
- https://chatgpt.com - No generated code was used, it was used for logic and syntax.
