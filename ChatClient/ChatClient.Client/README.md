```
Author:     Chase Hammond
Partner:    Teagan Smith
Start Date: 01-Nov-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  tweagan11, chammond123
Repo:       https://github.com/uofu-cs3500-20-fall2024/assignment-eight-chatting-bros_before_nodes_game
Commit Date:11/08/2024
Solution:   Chatting
Copyright:  CS 3500 and Teagan Smith, Chase Hammond - This work may not be copied for use in Academic Coursework.
```


# Overview of the ChatClient GUI Functionality
This program represents the GUI that a client will see when staring the program.  It gives the user the ablilty to connect to 
a given server with an IP address and port number, send messages through a text box and disconnect from the server.

# Design Decisions
The design decisions we decided to implement are: 

- A text box that clears after the message is sent.  Do accomplish this we added a new private method that handles a key press
  as well as a private member that stores the text input.  The message now sends on an "Enter" key press.
- The chat will clear when a user disconnects
- When a user join the chat they will be sent the last 100 messages sent to that chat.

# Time to completion notes
All time spent on this assignment was done with pair programming.
The expected and actual times represent the input of both parters.

Expected time: 7h

Time Spent Learning: .5 hr
Time Spent Manually Testing: .5 hr
Time Spent Creating: 3 hrs
Time Spent Debugging:  1.5 hr

Actual Time: 5.5h

# Comments to evaluators:
### BIG BUG:
- One bug we ran into that took us a second to figure out was that when a different user disconnected, the server disconnected the current user (or any current users.)
After some deeper research we saw that there was no updating in the currConnections of each user if a user left, leaving a connection that was closed that the users still tried to
talk to. We fixed this by putting our currConnections in the while loop and starting fresh each time, since connections has an updated copy of the connections.
-Chats are send with the enter key.

# Peer Programming:
Both parters were driven and committed to getting this assignment completed on time and at A quality. There weren't any disconnects in communication
or effort.  We both feel happy with how the pair programing proceeded.

# Consulted peers:
Chase Hammond
Teagan Smith
Jacob Erard

# References:
- https://chatgpt.com - No generated code was used, it was used for logic and syntax.