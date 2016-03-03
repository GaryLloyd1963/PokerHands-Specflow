using System.Linq;

namespace PokerHands
{
    public class HandEvaluator : IHandEvaluator
    {
        private const string CardValuesAceIsHigh = "..23456789TJQKA";
        private const string CardValuesAceIsLow = ".A23456789TJQK";
        private const string CardSuits = "SCDH";

        public int StraightFlushValue(string hand)
        {
            var highestRank = StraightValue(hand);
            return FlushValue(hand) == Constants.NO_VALUE ? Constants.NO_VALUE : highestRank;
        }

        public int FourOfAKindValue(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValuesAceIsHigh.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand) == 4)
                    return cardIdx;
            }
            return Constants.NO_VALUE;
        }

        public int FullHouseValue(string hand)
        {
            var pairFound = false;
            var tripsFound = false;
            var tripsValue = Constants.NO_VALUE;
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
            return tripsFound && pairFound ? tripsValue : Constants.NO_VALUE;
        }

        public int FlushValue(string hand)
        {
            return CardSuits.Any(t => CountCardSymbols(t, hand) == Constants.CARDS_IN_HAND)
                                                ? HighestRankAceIsHigh(hand) : Constants.NO_VALUE;

        }

        public int StraightValue(string hand)
        {
            var straightValue = StraightValueAceIsHigh(hand);
            if (straightValue != Constants.NO_VALUE)
                return straightValue;
            straightValue = StraightValueAceIsLow(hand);
            return straightValue != Constants.NO_VALUE ? straightValue : Constants.NO_VALUE;
        }

        private int StraightValueAceIsHigh(string hand)
        {
            if (DuplicateRankValuesInHand(hand))
                return Constants.NO_VALUE;
            var lowestCardRank = LowestRank(hand, CardValuesAceIsHigh);
            var highestCardRank = HighestRank(hand, CardValuesAceIsHigh);
            return highestCardRank - lowestCardRank == 4 ? highestCardRank : Constants.NO_VALUE;
        }

        private int StraightValueAceIsLow(string hand)
        {
            if (DuplicateRankValuesInHand(hand))
                return Constants.NO_VALUE;
            var lowestCardRank = LowestRank(hand, CardValuesAceIsLow);
            var highestCardRank = HighestRank(hand, CardValuesAceIsLow);
            return highestCardRank - lowestCardRank == 4 ? highestCardRank : Constants.NO_VALUE;
        }

        public int TripsValue(string hand)
        {
            for (var cardIdx = 0; cardIdx < CardValuesAceIsHigh.Length; cardIdx++)
            {
                if (CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand) == 3)
                    return cardIdx;
            }
            return Constants.NO_VALUE;
        }

        public int[] TwoPairsValues(string hand)
        {
            var highPairValue = Constants.NO_VALUE;
            var lowPairValue = Constants.NO_VALUE;
            var kickerValue = Constants.NO_VALUE;

            for (var cardIdx = CardValuesAceIsHigh.Length - 1; cardIdx >= 0; cardIdx--)
            {
                var count = CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand);
                if (count == 2)
                {
                    if (highPairValue == Constants.NO_VALUE)
                        highPairValue = cardIdx;
                    else
                        lowPairValue = cardIdx;
                }
                else if (count == 1)
                {
                    kickerValue = cardIdx;
                }
            }
            return new[] { highPairValue, lowPairValue, kickerValue };
        }

        public int[] PairValues(string hand)
        {
            var pairRankValue = Constants.NO_VALUE;
            var firstKicker = Constants.NO_VALUE;
            var secondKicker = Constants.NO_VALUE;
            var thirdKicker = Constants.NO_VALUE;

            for (var cardIdx = CardValuesAceIsHigh.Length - 1; cardIdx >= 0; cardIdx--)
            {
                var count = CountCardSymbols(CardValuesAceIsHigh[cardIdx], hand);
                if (count == 2)
                {
                    if (pairRankValue == Constants.NO_VALUE)
                        pairRankValue = cardIdx;
                }
                else if (count == 1)
                {
                    if (firstKicker == Constants.NO_VALUE)
                        firstKicker = cardIdx;
                    else if (secondKicker == Constants.NO_VALUE)
                        secondKicker = cardIdx;
                    else if (thirdKicker == Constants.NO_VALUE)
                        thirdKicker = cardIdx;
                    else
                        break;
                }
            }
            return new[] { pairRankValue, firstKicker, secondKicker, thirdKicker };
        }

        private static int LowestRank(string hand, string cardValues)
        {
            for (var cardIdx = 0; cardIdx < cardValues.Length; cardIdx++)
            {
                if (CountCardSymbols(cardValues[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return Constants.NO_VALUE;
        }

        public int HighestRankAceIsHigh(string hand)
        {
            return HighestRank(hand, CardValuesAceIsHigh);
        }

        private int HighestRank(string hand, string cardValues)
        {
            for (var cardIdx = cardValues.Length - 1; cardIdx >= 0; cardIdx--)
            {
                if (CountCardSymbols(cardValues[cardIdx], hand) > 0)
                    return cardIdx;
            }
            return Constants.NO_VALUE;
        }
        private bool DuplicateRankValuesInHand(string hand)
        {
            return CardValuesAceIsHigh.Any(t => CountCardSymbols(t, hand) > 1);
        }

        private static int CountCardSymbols(char cardSymbol, string hand)
        {
            return hand.Count(t => cardSymbol == t);
        }

    }
}