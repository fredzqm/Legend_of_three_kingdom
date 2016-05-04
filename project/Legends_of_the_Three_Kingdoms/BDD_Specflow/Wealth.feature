Feature: Wealth
	I want my player can use wealth

@mytag
Scenario: Wealth 
	Given There is a game of player A 
	And At Player A's actionPhase2
	When Player A uses Wealth
	Then Player A has 3 cards
