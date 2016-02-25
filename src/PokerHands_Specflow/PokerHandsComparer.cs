using System.Linq;

namespace PokerHands
{
    public class PokerHandsComparer
    {
        private const string CardValues = "..23456789TJQKA";

        public string CompareHands(string firstPlayer, string firstPlayerCards, string secondPlayer, string secondPlayerCards)
        {
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

        private int HighestRank(string hand)
        {
            return 0;
        }

        private int HandIsAStraight(string hand)
        {
            return 0;
        }

        private static int HandIsFourOfAKind(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValues.Length; cardIdx++)
            {
                if (CountCardRank(CardValues[cardIdx], hand) == 4)
                    return cardIdx;
            }
            return 0;
        }

        private static int CountCardRank(char cardValue, string hand)
        {
            return hand.Count(t => cardValue == t);
        }
    }
}