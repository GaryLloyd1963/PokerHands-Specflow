Feature: PokerHands
	In order to build a superb online poker casino
	As a developer
	I want to be able to compared 2 poker hands

Scenario: Given 2 high card hands held by players Black and White
	Given Black's hand is 2H 3D 5S 9C KD
	And White's hand is 2C 3H 4S 8C AH
	When I compare the hands
	Then the result is "White wins. - with high card: Ace"
