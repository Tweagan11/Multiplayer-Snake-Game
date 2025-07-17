```
Author:     Chase Hammond
Partner:    Teagan Smith
Start Date: 22-Nov-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  tweagan11, chammond123
Repo:       https://github.com/uofu-cs3500-20-fall2024/assignment-eight-chatting-bros_before_nodes_game
Commit Date:11/22/2024
Solution:   SnakeGame
Copyright:  CS 3500 and Teagan Smith, Chase Hammond - This work may not be copied for use in Academic Coursework.
```


# Overview of the SnakeClient GUI.
This program represents the GUI that a client will use to interact with a game server along with the models used to represent the game world.
It gives the user the ability to connect to a given game server with an IP address and port number and connect with their player name.  
It then listens for user key inputs and sends them to the server to control the player snake.  There is a leader board containing the 
currently connected snakes with their scores.  There is an FPS counter displaying the current frames per second.

# Design Decisions
The design decisions we decided to implement are: 

- Textures for the wall and background objects.
- A leader board that shows the current playing snakes in descending order by score.  The leader board will resize as
  players connect and disconnect and prevents player names that are too long from clipping out of the box.
- When a snake dies it turns red.

# Time to completion notes
All time spent on this assignment was done with pair programming.
The expected and actual times represent the input of both parters.

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
