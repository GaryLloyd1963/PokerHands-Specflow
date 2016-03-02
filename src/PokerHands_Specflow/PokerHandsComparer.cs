using System.Linq;
using System.Linq.Expressions;

namespace PokerHands
{
    public class PokerHandsComparer
    {
        private const int CARDS_IN_HAND = 5;
        private const int NO_VALUE = 0;
        private string NO_RESULT = string.Empty;
        private const string CardValuesAceIsHigh = "..23456789TJQKA";
        private const string CardValuesAceIsLow = ".A23456789TJQK";
        private const string CardSuits = "SCDH";

        public string CompareHands(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var result = CheckForStraightFlush(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != NO_RESULT)
                return result;

            result = CheckFor4OfaKind(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != NO_RESULT)
                return result;

            result = CheckForAFullHouse(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != NO_RESULT)
                return result;

            result = CheckForAStraight(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != NO_RESULT)
                return result;

            if (firstPlayerCards.Contains("A"))
                return string.Format("{0} wins - with high card: Ace", firstPlayerName);
            if (secondPlayerCards.Contains("A"))
                return string.Format("{0} wins - with high card: Ace", secondPlayerName);
            return "Tie";
        }

        private string CheckForStraightFlush(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var firstStraightFlushValue = StraightFlushRankValue(firstPlayerCards);
            var secondStraightFlushValue = StraightFlushRankValue(secondPlayerCards);
            if (firstStraightFlushValue > secondStraightFlushValue)
                return (string.Format("{0} wins - straight flush", firstPlayerName));
            if (secondStraightFlushValue > firstStraightFlushValue)
                return (string.Format("{0} wins - straight flush", secondPlayerName));
            if (firstStraightFlushValue > NO_VALUE && secondStraightFlushValue > NO_VALUE)
                return "Tie";
            return NO_RESULT;
        }

        private string CheckFor4OfaKind(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var firstHandFourOfAKindValue = FourOfAKindRankValue(firstPlayerCards);
            var secondHandFourOfAKindValue = FourOfAKindRankValue(secondPlayerCards);
            if (firstHandFourOfAKindValue > secondHandFourOfAKindValue)
                return (string.Format("{0} wins - four of a kind", firstPlayerName));
            if (secondHandFourOfAKindValue > firstHandFourOfAKindValue)
                return (string.Format("{0} wins - four of a kind", secondPlayerName));
            return NO_RESULT;
        }

        private string CheckForAFullHouse(string firstPlayerName, string firstPlayerCards, string secondPlayerName,
            string secondPlayerCards)
        {
            var firstFullHouseRankValue = FullHouseRankValue(firstPlayerCards);
            var secondFullHouseRankValue = FullHouseRankValue(secondPlayerCards);
            if (firstFullHouseRankValue > secondFullHouseRankValue)
                return (string.Format("{0} wins - full house", firstPlayerName));
            if (secondFullHouseRankValue > firstFullHouseRankValue)
                return (string.Format("{0} wins - full house", secondPlayerName));
            return NO_RESULT;
        }

        private string CheckForAStraight(string firstPlayerName, string firstPlayerCards, string secondPlayerName,
            string secondPlayerCards)
        {
            var firstStraightRankValue = HandIsStraight(firstPlayerCards);
            var secondStraightRankValue = HandIsStraight(secondPlayerCards);
            if (firstStraightRankValue > secondStraightRankValue)
                return (string.Format("{0} wins - straight", firstPlayerName));
            if (secondStraightRankValue > firstStraightRankValue)
                return (string.Format("{0} wins - straight", secondPlayerName));
            if (firstStraightRankValue > NO_VALUE)
                return "Tie";
            return NO_RESULT;
        }

        private int FullHouseRankValue(string hand)
        {
            var pairFound = false;
            var tripsFound = false;
            var tripsValue = NO_VALUE;
            for (var cardIdx = 0; cardIdx < CardValuesAceIsHigh.Length; cardIdx++)
            {
                var count = CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand);
                if (count == 3)
                {
                    tripsFound = true;
                    tripsValue = cardIdx;
                }
                else if (count == 2)
                    pairFound = true;
            }
            return tripsFound && pairFound? tripsValue : NO_VALUE;
        }

        private int StraightFlushRankValue(string hand)
        {
            if (!HandIsFlush(hand))
                return 0;
            var highestRank = HandIsStraight(hand);
            return highestRank > NO_VALUE ? highestRank : NO_VALUE;
        }

        private bool HandIsFlush(string hand)
        {
            return CardSuits.Any(t => CountCardSymbols(t, hand) == CARDS_IN_HAND);
        }

        private int HandIsStraight(string hand)
        {
            var straightValue = HandIsStraightAceIsHigh(hand);
            if (straightValue > NO_VALUE)
                return straightValue;
            straightValue = HandIsStraightAceIsLow(hand);
            return straightValue > NO_VALUE ? straightValue : NO_VALUE;
        }
        private int HandIsStraightAceIsHigh(string hand)
        {
            var lowestCardRank = LowestRankAceIsHigh(hand);
            var highestCardRank = HighestRankAceIsHigh(hand);
            return highestCardRank - lowestCardRank == 4 ? highestCardRank : NO_VALUE;
        }

        private int HandIsStraightAceIsLow(string hand)
        {
            var lowestCardRank = LowestRankAceIsLow(hand);
            var highestCardRank = HighestRankAceIsLow(hand);
            return highestCardRank - lowestCardRank == 4 ? highestCardRank : NO_VALUE;
        }

        private int LowestRankAceIsHigh(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValuesAceIsHigh.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int LowestRankAceIsLow(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValuesAceIsLow.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValuesAceIsLow[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int HighestRankAceIsHigh(string hand)
        {
            for (var cardIdx = CardValuesAceIsHigh.Length - 1; cardIdx >= 0; cardIdx--)
            {
                if (CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int HighestRankAceIsLow(string hand)
        {
            for (var cardIdx = CardValuesAceIsLow.Length - 1; cardIdx >= 0; cardIdx--)
            {
                if (CountCardSymbols(CardValuesAceIsLow[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int FourOfAKindRankValue(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValuesAceIsHigh.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand) == 4)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int CountCardSymbols(char cardSymbol, string hand)
        {
            return hand.Count(t => cardSymbol == t);
        }
    }
}