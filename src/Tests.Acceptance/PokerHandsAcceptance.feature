Feature: PokerHands
	In order to build a superb online poker casino
	As the program developer
	I want to be able to compare 2 poker hands

#=================================================================================================================
# Straight flush tests
Scenario: Straight Flush. Given a low straight flush and a high straight flush dealt to players Black and White
	Given the hand dealt to Black is '4S 5S 6S 7S 8S'
	And the hand dealt to White is  '9C TC JC QC KC'
	When I compare the hands
	Then the comparison result is 'White wins - straight flush'

Scenario: Straight Flush. Given a high straight flush and a low straight flush dealt to players Black and White
	Given the hand dealt to Black is '8S 9S TS JS QS'
	And the hand dealt to White is  '4D 5D 6D 7D 8D'
	When I compare the hands
	Then the comparison result is 'Black wins - straight flush'

Scenario: Straight Flush. Given a 4 of a kind hand and a straight flush dealt to players Black and White
	Given the hand dealt to Black is '2H 2D 2S 2C KD'
	And the hand dealt to White is  '4C 5C 6C 7C 8C'
	When I compare the hands
	Then the comparison result is 'White wins - straight flush'

Scenario: Straight Flush. Given a 2 equal straight flushes dealt to players Black and White
	Given the hand dealt to Black is '4D 5D 6D 7D 8D'
	And the hand dealt to White is  '4C 5C 6C 7C 8C'
	When I compare the hands
	Then the comparison result is 'Tie'

#=================================================================================================================
# Four of a kind tests	
Scenario: Four of a kind. Given a high 4 of a kind and a low 4 of a kind dealt to players Black and White
	Given the hand dealt to Black is 'KH KS KC KD 2C'
	And the hand dealt to White is  '3H 3S 3C 3D 8S'
	When I compare the hands
	Then the comparison result is 'Black wins - four of a kind'

Scenario: Four of a kind. Given an Ace high and a 4 of a kind dealt to players Black and White
	Given the hand dealt to Black is 'AH 2S KC 4D 6S'
	And the hand dealt to White is  '2C 7H 7S 7C 7D'
	When I compare the hands
	Then the comparison result is 'White wins - four of a kind'

Scenario: Four of a kind. Given a low 4 of a kind and a high 4 of a kind dealt to players Black and White
	Given the hand dealt to Black is '5H 5S 5C 5D 9S'
	And the hand dealt to White is  'AH AS AC AD 6C'
	When I compare the hands
	Then the comparison result is 'White wins - four of a kind'

#=================================================================================================================
# Full house tests
Scenario: Full house. Given a high card hand and a full house dealt to players Black and White
	Given the hand dealt to Black is '6H 3D AH 2D 5C'
	And the hand dealt to White is  '8H 8D 8C KD KH'
	When I compare the hands
	Then the comparison result is 'White wins - full house'

Scenario: Full house. Given a full house and a high card hand dealt to players Black and White
	Given the hand dealt to Black is '4C 6D 6H 4H 4D'
	And the hand dealt to White is  '2H 3D KC QD AH'
	When I compare the hands
	Then the comparison result is 'Black wins - full house'

#=================================================================================================================
# Flush tests
Scenario: Flush. Given a high card and a flush dealt to players Black and White
	Given the hand dealt to Black is '2H 4S 8C 7D AH'
	And the hand dealt to White is  '2S 8S AS QS 3S'
	When I compare the hands
	Then the comparison result is 'White wins - flush'

Scenario: Flush. Given a flush and a high card hand dealt to players Black and White
	Given the hand dealt to Black is '3H 2H KH QH 7H'
	And the hand dealt to White is  '2D 4S 6C 7D AH'
	When I compare the hands
	Then the comparison result is 'Black wins - flush'

Scenario: Flush. Given 2 unequal flushes dealt to players Black and White
	Given the hand dealt to Black is '3H 2H KH QH 7H'
	And the hand dealt to White is  '2D 4D 7D QD AD'
	When I compare the hands
	Then the comparison result is 'White wins - flush'

Scenario: Flush. Given 2 equal flushes dealt to players Black and White
	Given the hand dealt to Black is '3H 2H KH QH 7H'
	And the hand dealt to White is  'KC 3C 7C 2C QC'
	When I compare the hands
	Then the comparison result is 'Tie'

#=================================================================================================================
# Straight tests
Scenario: Straight. Given a high card hand, and a straight, dealt to players Black and White
	Given the hand dealt to Black is '2H 3D 5S 9C AD'
	And the hand dealt to White is  'AC 2D 3S 4C 5H'
	When I compare the hands
	Then the comparison result is 'White wins - straight'

Scenario: Straight. Given a straight and a high card hand dealt to players Black and White
	Given the hand dealt to Black is '2H 3D 4S 5C 6D'
	And the hand dealt to White is  'KC 2D 3S 4C 5H'
	When I compare the hands
	Then the comparison result is 'Black wins - straight'

Scenario: Straight. Given 2 equal straights dealt to players Black and White
	Given the hand dealt to Black is '2H 3D 4S 5C 6D'
	And the hand dealt to White is  '2C 3H 4C 5H 6C'
	When I compare the hands
	Then the comparison result is 'Tie'

#=================================================================================================================
# Three of a kind tests (trips)
Scenario: Three of a kind. Given a high card hand, and trips, dealt to players Black and White
	Given the hand dealt to Black is '2H 3D 5S 9C AD'
	And the hand dealt to White is  'AC AD AS 4C 5H'
	When I compare the hands
	Then the comparison result is 'White wins - trips'

Scenario: Three of a kind. Given trips and a high card hand dealt to players Black and White
	Given the hand dealt to Black is '3C 3D 3S KC AH'
	And the hand dealt to White is  '2H 3D 5S 9C AD'
	When I compare the hands
	Then the comparison result is 'Black wins - trips'

Scenario: Three of a kind. Given trips dealt to players Black and White
	Given the hand dealt to Black is 'KC KD KS 2C AH'
	And the hand dealt to White is  'JC JD JS 8C 4H'
	When I compare the hands
	Then the comparison result is 'Black wins - trips'

#=================================================================================================================
# Two pair tests
Scenario: Two pairs. Given a high card hand and a two pair dealt to players Black and White
	Given the hand dealt to Black is '2H 5S KD 9C 3D'
	And the hand dealt to White is  'KC KH 4S 4C AH'
	When I compare the hands
	Then the comparison result is 'White wins - two pairs'

Scenario: Two pairs. Given a two pairs hand and a high card hand dealt to players Black and White
	Given the hand dealt to Black is 'KC KH 4S 4C AH'
	And the hand dealt to White is  '2H 5S KD 9C 3D'
	When I compare the hands
	Then the comparison result is 'Black wins - two pairs'

Scenario: Two pairs. Given a equal two pairs hands with unequal kickers dealt to players Black and White
	Given the hand dealt to Black is 'KC KH 4S 4C 2H'
	And the hand dealt to White is  'KD KS 4D 4H 8H'
	When I compare the hands
	Then the comparison result is 'White wins - two pairs'

Scenario: Two pairs. Given a equal two pairs hands with equal kickers dealt to players Black and White
	Given the hand dealt to Black is 'KC KH 4S 4C 8C'
	And the hand dealt to White is  'KD KS 4D 4H 8H'
	When I compare the hands
	Then the comparison result is 'Tie'

#=================================================================================================================
# Pair tests
Scenario: Pairs. Given a high card hand and a pair dealt to players Black and White
	Given the hand dealt to Black is '2H 5S KD 9C 3D'
	And the hand dealt to White is  '3C KC KH 4S QH'
	When I compare the hands
	Then the comparison result is 'White wins - pair'

Scenario: Pairs. Given a pair and a high card hand dealt to players Black and White
	Given the hand dealt to Black is '3C KC KH 4S QH'
	And the hand dealt to White is  '2H 5S KD 9C 3D'
	When I compare the hands
	Then the comparison result is 'Black wins - pair'

Scenario: Pairs. Given pair hands dealt to players Black and White
	Given the hand dealt to Black is '3C KC KH 4S QH'
	And the hand dealt to White is  '2H 5S 5D 9C 3D'
	When I compare the hands
	Then the comparison result is 'Black wins - pair'

#=================================================================================================================
# High card tests	
Scenario: High card. Given 2 high card hands, king high, and ace high, dealt to players Black and White
	Given the hand dealt to Black is '2H 3D 5S 9C KD'
	And the hand dealt to White is  '2C 3H 4S 8C AH'
	When I compare the hands
	Then the comparison result is 'White wins - high card'

Scenario: High card. Given 2 high card hands, ace high, and king high, dealt to players Black and White
	Given the hand dealt to Black is '2C 3H 4S 8C AH'
	And the hand dealt to White is  '2H 3D 5S 9C KD'
	When I compare the hands
	Then the comparison result is 'Black wins - high card'





