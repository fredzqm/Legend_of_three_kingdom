Milestone2:
YunYan Ding:
I did the code in ModelTest package. I wrote the test code. All the tests contain the process of checking the data and the exception. I build the exception handler. There are mainly three types of exceptions that we meet until now. Most of the tests followed TDD, except some of them cannot do that because of the branch merge problem.

Fred Zhang:
I build the main frame of the game model, including CardSet, Card, Game, Phase, PhaseList, Plyaer, UserAction, UserAction and GameController.
PhaseList is a simple linked list used to store game phases.
CardSet is a cardpile machine that handles draw and discard cards.
Player encodes player's reaction in each phase. Since each different character can have different behavior, this class will be overridden. 


Songyu Wang:
I did the codes in the View package. I built the GUI. All the buttons have functions as "XX_Click". This is the event listeners. Also, together, Fred and I changed the codes to match each other's code. As the Result, by clicking "Ok" or "Cancle", the text of stage changes. This is our performs system test.  

Milestone3:
Fred Zhang:
This week I implement all the logic for Attack and Miss. I also written logics for AskForHelpPhase when a player is on the edge of dying.
Phase is the central logic unit of this game. I have completed several kinds of Phase,
including HiddenPhase and VisiblePhase.
HiddenPhase are those Phases processing internal game logic. The user should not know the
HiddenPhase. VisiblePhase are those Phases that the player are aware of. Some of them is just a delay -- PausePhase. Some of them requires the player to do something -- UserActionPhase.
Furthermore, I discovered that in many cases, a player needs to just respond with a certain kind of card. So I create a simple Inerface to handle all of those similar cases. ResponsePhase & NeedResponsePhase.

I also complete the Phase

Songyu Wang:
This week I changed the form a little bit to display more information. Also, one more window is built. Bugs on GUI side are fixed. Fred and I changed the codes to match each other's code. Right now we are able to display the result health after being attacked by a another player. It means all the buttons are functional. 