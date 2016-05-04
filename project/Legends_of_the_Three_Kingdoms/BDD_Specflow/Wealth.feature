Feature: Wealth
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Wealth 
	Given There is a game of player A 
	And At Player A's actionPhase
	When Player A uses Wealth
	Then Player A has 2 cards
