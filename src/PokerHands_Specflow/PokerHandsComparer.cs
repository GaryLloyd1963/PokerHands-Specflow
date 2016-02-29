using System.Linq;
using System.Linq.Expressions;

namespace PokerHands
{
    public class PokerHandsComparer
    {
        private const int NO_VALUE = 0;
        private string NO_RESULT = string.Empty;
        private const string CardValues = "..23456789TJQKA";
        private const string CardSuits = "SCDH";

        public string CompareHands(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var result = CheckForStraightFlush(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != string.Empty)
                return result;

            result = CheckFor4OfaKind(firstPlayerName, firstPlayerCards, secondPlayerName, secondPlayerCards);
            if (result != string.Empty)
                return result;

            if (firstPlayerCards.Contains("A"))
                return string.Format("{0} wins. - with high card: Ace", firstPlayerName);
            if (secondPlayerCards.Contains("A"))
                return string.Format("{0} wins. - with high card: Ace", secondPlayerName);
            return "Tie";
        }

        private string CheckForStraightFlush(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var firstStraightFlushValue = StraightFlushRankValue(firstPlayerCards);
            var secondStraightFlushValue = StraightFlushRankValue(secondPlayerCards);
            if (firstStraightFlushValue > secondStraightFlushValue)
                return (string.Format("{0} wins. - straight flush", firstPlayerName));
            if (secondStraightFlushValue > firstStraightFlushValue)
                return (string.Format("{0} wins. - straight flush", secondPlayerName));
            if (firstStraightFlushValue > NO_VALUE && secondStraightFlushValue > NO_VALUE)
                return "Tie";
            return NO_RESULT;
        }

        private string CheckFor4OfaKind(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var firstHandFourOfAKindValue = FourOfAKindRankValue(firstPlayerCards);
            var secondHandFourOfAKindValue = FourOfAKindRankValue(secondPlayerCards);
            if (firstHandFourOfAKindValue > secondHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", firstPlayerName));
            if (secondHandFourOfAKindValue > firstHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", secondPlayerName));
            return NO_RESULT;
        }

        private string CheckForAFullHouse(string firstPlayerName, string firstPlayerCards, string secondPlayerName,
            string secondPlayerCards)
        {
            var firstFullHouseRankValue = FullHouseRankValue(firstPlayerCards);
            var secondFullHouseRankValue = FullHouseRankValue(secondPlayerCards);
            return NO_RESULT;
        }

        private int FullHouseRankValue(string cards)
        {
            return NO_VALUE;
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
            return CardSuits.Any(t => CountCardSymbols(t, hand) == 5);
        }

        private int HandIsStraight(string hand)
        {
            var lowestCardRank = LowestRank(hand);
            var highestCardRank = HighestRank(hand);
            return highestCardRank - lowestCardRank == 4 ? highestCardRank : 0;
        }

        private int LowestRank(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValues.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValues[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int HighestRank(string hand)
        {
            for (var cardIdx = CardValues.Length - 1; cardIdx >= 0; cardIdx--)
            {
                if (CountCardSymbols(CardValues[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return NO_VALUE;
        }

        private int FourOfAKindRankValue(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValues.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValues[cardIdx], hand) == 4)
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