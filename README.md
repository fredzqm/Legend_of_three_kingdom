## Milestone2: 
YunYan Ding:
I did the code in ModelTest package. I wrote the test code. All the tests contain the process of checking the data and the exception. I build the exception handler. There are mainly three types of exceptions that we meet until now. Most of the tests followed TDD, except some of them cannot do that because of the branch merge problem.

Fred Zhang:
I build the main frame of the game model, including CardSet, Card, Game, Phase, PhaseList, Plyaer, UserAction, UserAction and GameController.
PhaseList is a simple linked list used to store game phases.
CardSet is a cardpile machine that handles draw and discard cards.
Player encodes player's reaction in each phase. Since each different character can have different behavior, this class will be overridden. 


Songyu Wang:
I did the codes in the View package. I built the GUI. All the buttons have functions as "XX_Click". This is the event listeners. Also, together, Fred and I changed the codes to match each other's code. As the Result, by clicking "Ok" or "Cancle", the text of stage changes. This is our performs system test.  

## Milestone3:
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

## Milestone4:
Fred Zhang:
This week I add some sugars to the GUI interface. It now displayes logs to the scroll panel in the middle.
I also add timer system to the model, so that if the player is not doing anything, his reaction phase timeout and autoadvanced. If the player obviously only has one choice, the game can automatically advanced also.
I also use polymorphism instead of case switch when userActioPhase handle userActions.
I tested all the features we have, including characters abilities.

Songyu Wang:
This week I implement all five characters ability. I used TDD, BVA and Mocking to test the code I wrote. And All code pass tests. I delete some tests due to the change of requirement. To write the five abilities, I refactors some of the code Fred wrote to get the value I need. Also, I override some his functions. I added all the documentation for Model and Controller.

## Milestone5:
Fred Zhang:
In this week, separate part of code in Gamecontroller and create an UserActionHandler, so this part of the logic is testable.
I use decision table and paramatized testing method to test how controller handle user input. I catch a few bugs with the test, and throw proper exception.
Those exception will be caught and a nice error message will be displayed in GUI.

Songyu Wang:
This week I tried to make the GUI look nicer. So I added one background picture and five players pictures. Also, I changed the fonts and colors of text and the color of components. This week, we did not implement any new feature, so I did not proform system and exploratory testing. 


## Milestone6:
Fred zhang:
This week I implemented the player dying, asking for help and end game feature. When one player gets hurt and has 0 health, he would ask everyone for help. If any of them can save him with sufficient number of peaches, the dying player survive, otherwise he is dead. If some player is dead, the game check if the game had ended.
I use TDD to create test for all newly added methods and classes and achieve 100% statement coverage as indicated by visual studio. I use parameterized test for testing end game logic. It worked well.
I also integrated GUI with the model, so the player can see who is dead.
Songyu Wang:
I tried to add tests for previous milestones code. There are too many codes, I wrote about 70 tests but I still not be able to achieve 100% coverage. Also, I tried to install the codecoverage on gitlab, but It seems like this tools does not support Mocking. So I cannot install it. 

## Milestone7:
Fred Zhang:
This week, I used BDD to implement wine and peach used in actionphase. After use wine, the next attack has one extra hurt. When the player is hurt, he can use a peach to gain one health.
I also implement the timer to display the time left for each player to response. As we can see, when the timer counts down to zero, the player's response phase timeout and default behaviour is applied.

Songyu wang:
This week, I use BDD implemented Wealth card and Peachgarden card. All the tests pass. Also, I tested the code I wrote through GUI and it also works.

## Milestone8:
Fred Zhang:
This week, we write the fuzz test random generator together.
To make sure that fuzzing testing shows what's going on, I create an logEvent in game, so it broadcast all listners what happened.
Therefore, we can redirect the logs from GUI window to standard out. The test can have outputs showing what went wrong.

Songyu Wang:
This week, we apply fuzz test to our project. I wrote half of the random generator. Also, I add some logs format to allow us knowing better what is going wrong.

# Final:
completed features:

1. This game needs five players. Each player will randomly receive a character card.

	Liu Bei: Liu Bei's can give any number of his hand cards to any players. If he gives away more than one card, he recovers one unit of health.
	
	Zhang Fei: Zhang Fei has no restrictions on how many times he can attack during his turn.
	
	Cao Cao: When Cao Cao is damaged by a card, he can immediately put it into his hand.
	
	Sun Quan: Once during his turn, Sun Quan can discard any number of cards to draw the same number.
	
	lu Meng: If Lu Meng does not use any Attack cards during his turn, he can skip his discard phase.
	
2. Game start rules

	Each player will play as king, rebel, loyalist or spy. In one game, there is usually one king, one loyalist, one spy and two rebels. Each play is assigned an identity and character statically in GameController. Each player draws four initial cards.
	Then the game starts with the king’s round and go counterclockwise.
	
3. Game ending rules (objective of each identity)

	The king and the loyalist are on the same team. Their objective is to protect the king and kill rebels and spy (everybody else) at the very end. If the king dies, they lose the game immediately. As long as the king survive, the loyalist does not have to survive to win the game. 
	
	A player gains nothing for killing a loyalist, but if the king kills a loyalist, the king loses all his cards.
	The rebels form one team. Their objective is to kill the king. When the king dies, and there are more than two people left or there is any rebel left, the rebels win. 
	
	Someone who killed a rebel can draw three cards as the rewards.
	
	The spy is all on his own even if there are two spies. The spy wants to kill all other players, and then kill the king at the very end. The spy has to protect the king until there are only two players (the spy and the king) left.
	Players do not gain anything for killing the spy.
	
4. Each player has four stages in one round.

	* Judgment Phase: If there are delay tools (Capture and Starvation) on the player, judge them sequentially. (Judge: show the top card in the stack, and perform specific operations based on the delay tools).

	* Drawing Phase: The player draw two cards in this phase.
	
	* Action Phase: Here, players can play cards from their hand or use character abilities.  Players can use any number of tool or equipment cards, but they can only use one attack and one wine. The player can choose the order to use their cards or trigger character ability (The very fun part).
	
	* Discard Phase: Specific character ability might be triggered in this phase. By default, the player can only keep only as many hand cards as they have health and is forced to discard the rest.
	
5. Based on the game rules, we decided to implement some cards from the official cards domain to keep the project in a appropriate size (all the cards have four suits). 

	Basic cards are:
	
	* “Attack”: During their action phase, a player can use at most one “attack” towards any player. They can play an escape or 			suffer one damage. 
	* ”Escape”: When attacked, a player use an “escape” to avoid damage.
	
	* “Peach”: During their action phase, a player can use a peach to increase their health by one. When a dying player requests 			help, the player can use “peach” to give them one unit of health. (See death rules)
	
	* “Wine”: During their action phase, a player can use at most one “wine”. If they do, the damage of the next attack in the 			same round causes one more damage.
		
	Tool cards are:
	
	* “PeachGarden”: The player targets all player, let them restore one heath (not above health limit).
	
	* “Wealth”: The player targets himself, draws two cards.
		
6. Each player play one round and move to next player.
7. If any player win, game over.

Songyu Wang:
This week, I translated the game from English to Chinese. Now The game can display different languages based on the system language. 

Fred Zhang:
This week, I did the performance analysis of the game. It turns out that the majority of the CPU usage is not even in the code we generate, but the code C# used to maintain GUI, so I guess we don't have a performance issue. 
