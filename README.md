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

Milestone4:
Fred Zhang:
This week I add some sugars to the GUI interface. It now displayes logs to the scroll panel in the middle.
I also add timer system to the model, so that if the player is not doing anything, his reaction phase timeout and autoadvanced. If the player obviously only has one choice, the game can automatically advanced also.
I also use polymorphism instead of case switch when userActioPhase handle userActions.
I tested all the features we have, including characters abilities.

Songyu Wang:
This week I implement all five characters ability. I used TDD, BVA and Mocking to test the code I wrote. And All code pass tests. I delete some tests due to the change of requirement. To write the five abilities, I refactors some of the code Fred wrote to get the value I need. Also, I override some his functions. I added all the documentation for Model and Controller.

Milestone5:
Fred Zhang:
In this week, separate part of code in Gamecontroller and create an UserActionHandler, so this part of the logic is testable.
I use decision table and paramatized testing method to test how controller handle user input. I catch a few bugs with the test, and throw proper exception.
Those exception will be caught and a nice error message will be displayed in GUI.

Songyu Wang:
This week I tried to make the GUI look nicer. So I added one background picture and five players pictures. Also, I changed the fonts and colors of text and the color of components. This week, we did not implement any new feature, so I did not proform system and exploratory testing. 


Milestone6:
Fred zhang:
This week I implemented the player dying, asking for help and end game feature. When one player gets hurt and has 0 health, he would ask everyone for help. If any of them can save him with sufficient number of peaches, the dying player survive, otherwise he is dead. If some player is dead, the game check if the game had ended.
I use TDD to create test for all newly added methods and classes and achieve 100% statement coverage as indicated by visual studio. I use parameterized test for testing end game logic. It worked well.
I also integrated GUI with the model, so the player can see who is dead.
Songyu Wang:
I tried to add tests for previous milestones code. There are too many codes, I wrote about 70 tests but I still not be able to achieve 100% coverage. Also, I tried to install the codecoverage on gitlab, but It seems like this tools does not support Mocking. So I cannot install it. 
