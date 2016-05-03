Feature: Wine
	Use wine can increase hurt by 1

@mytag
Scenario: Add two numbers
	Given There is a game of player A with 4 health and player B has 4 health
	And At Player A's actionPhase
    And Player A uses Wine
	And Player A attack Player B
	When Player B response with cancel
	Then Player B should has 2 health