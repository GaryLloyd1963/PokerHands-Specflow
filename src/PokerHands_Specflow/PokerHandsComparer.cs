using System.Linq;

namespace PokerHands
{
    public class PokerHandsComparer
    {
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
            var firstStraightFlushValue = HandIsStraightFlush(firstPlayerCards);
            var secondStraightFlushValue = HandIsStraightFlush(secondPlayerCards);
            if (firstStraightFlushValue > secondStraightFlushValue)
                return (string.Format("{0} wins. - straight flush", firstPlayerName));
            if (secondStraightFlushValue > firstStraightFlushValue)
                return (string.Format("{0} wins. - straight flush", secondPlayerName));
            if (firstStraightFlushValue > 0 && secondStraightFlushValue > 0)
                return "Tie";
            return string.Empty;
        }

        private string CheckFor4OfaKind(string firstPlayerName, string firstPlayerCards, string secondPlayerName, string secondPlayerCards)
        {
            var firstHandFourOfAKindValue = HandIsFourOfAKind(firstPlayerCards);
            var secondHandFourOfAKindValue = HandIsFourOfAKind(secondPlayerCards);
            if (firstHandFourOfAKindValue > secondHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", firstPlayerName));
            if (secondHandFourOfAKindValue > firstHandFourOfAKindValue)
                return (string.Format("{0} wins. - four of a kind", secondPlayerName));
            return string.Empty;
        }

        private int HandIsStraightFlush(string hand)
        {
            if (!HandIsFlush(hand))
                return 0;
            var highestRank = HandIsStraight(hand);
            return highestRank > 0 ? highestRank : 0;
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