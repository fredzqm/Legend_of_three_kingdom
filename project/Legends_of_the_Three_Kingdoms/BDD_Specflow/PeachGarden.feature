Feature: PeachGarden
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: PeachGarden
	Given There is a game of player A with 1 health and player B has 4 health 
	And At Player A's actionPhase
	When Player A uses Peachgarden
	Then Player A should has 2 health
	And Player B should has 4 health
