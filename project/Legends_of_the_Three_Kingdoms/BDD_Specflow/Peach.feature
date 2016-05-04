Feature: Peach
	I want my player can use peach 

@mytag
Scenario: Peach 
	Given There is a game of player A with 4 health and player B has 4 health 
	And At Player A's actionPhase
	And Player A uses Wine
	And Player A attack Player B
	And Player B response with cancel
	And At Player B's actionPhase
	When Player B uses Peach
	Then Player B should has 3 health
