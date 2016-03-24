YunYan Ding:

Fred Zhang:
I build the main frame of the game model, including CardSet, Card, Game, Phase, PhaseList, Plyaer, UserAction, UserAction and GameController.
PhaseList is a simple linked list used to store game phases.
CardSet is a cardpile machine that handles draw and discard cards.
Player encodes player's reaction in each phase. Since each different character can have different behavior, this class will be overridden. 


Songyu Wang:
I did the codes in the View package. I built the GUI. All the buttons have functions as "XX_Click". This is the event listeners. Also, together, Fred and I changed the codes to match each other's code. As the Result, by clicking "Ok" or "Cancle", the text of stage changes. This is our performs system and exploratory test. 