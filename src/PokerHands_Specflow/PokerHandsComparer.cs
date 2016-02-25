using System.Linq;

namespace PokerHands
{
    public class PokerHandsComparer
    {
        private const string CardValues = "..23456789TJQKA";
        private const string CardSuits = "SCDH";

        public string CompareHands(string firstPlayer, string firstPlayerCards, string secondPlayer, string secondPlayerCards)
        {
            var firstStraightFlushValue = HandIsStraightFlush(firstPlayerCards);
            var secondStraightFlushValue = HandIsStraightFlush(secondPlayerCards);
            if ( firstStraightFlushValue > secondStraightFlushValue )
                return (string.Format("{0} wins. - straight flush", firstPlayer));
            if (secondStraightFlushValue > firstStraightFlushValue)
                return (string.Format("{0} wins. - straight flush", secondPlayer));

            var firstHandFourOfAKindValue = HandIsFourOfAKind(firstPlayerCards);
            var secondHandFourOfAKindValue = HandIsFourOfAKind(secondPlayerCards);
            if (firstHandFourOfAKindValue > secondHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", firstPlayer));
            if (secondHandFourOfAKindValue > firstHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", secondPlayer));

            if (firstPlayerCards.Contains("A"))
                return string.Format("{0} wins. - with high card: Ace", firstPlayer);
            if (secondPlayerCards.Contains("A"))
                return string.Format("{0} wins. - with high card: Ace", secondPlayer);
            return "Tie";
        }

        private int HandIsStraightFlush(string hand)
        {
            if (!HandIsFlush(hand))
                return 0;
            var HighestRank = HandIsStraight(hand);
            return HighestRank > 0 ? HighestRank : 0;
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
            return 0;
        }

        private int HighestRank(string hand)
        {
            for (var cardIdx = CardValues.Length - 1; cardIdx >= 0; cardIdx--)
            {
                if (CountCardSymbols(CardValues[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return 0;
        }

        private int HandIsAStraight(string hand)
        {
            return 0;
        }

        private int HandIsFourOfAKind(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValues.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValues[cardIdx], hand) == 4)
                    return cardIdx;
            }
            return 0;
        }

        private int CountCardSymbols(char cardSymbol, string hand)
        {
            return hand.Count(t => cardSymbol == t);
        }
    }
}