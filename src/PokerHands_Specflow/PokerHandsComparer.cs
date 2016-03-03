namespace PokerHands
{
    public class PokerHandsComparer
    {
        private int TWOPAIR_HIGHEST_PAIR = 0;
        private int TWOPAIR_LOWEST_PAIR = 1;
        private int TWOPAIR_KICKER = 2;

        private IHandEvaluator evaluator;
        private string firstPlayerName;
        private string secondPlayerName;
        private string firstPlayerCards;
        private string secondPlayerCards;

        public PokerHandsComparer(IHandEvaluator evaluator, string firstPlayerName
                                        , string secondPlayerName, string firstPlayerCards, string secondPlayerCards)
        {
            this.evaluator = evaluator;
            this.firstPlayerName = firstPlayerName;
            this.secondPlayerName = secondPlayerName;
            this.firstPlayerCards = firstPlayerCards;
            this.secondPlayerCards = secondPlayerCards;
        }

        public string CompareHands()
        {
            var result = CheckForStraightFlush();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckFor4OfaKind();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForAFullHouse();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForAFlush();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForAStraight();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForTrips();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForTwoPairs();
            if (result != Constants.NO_RESULT)
                return result;

            result = CheckForPair();
            if (result != Constants.NO_RESULT)
                return result;

            var highCardFirstPlayer = evaluator.HighestRankAceIsHigh(firstPlayerCards);
            var highCardSecondPlayer = evaluator.HighestRankAceIsHigh(secondPlayerCards);

            if (highCardFirstPlayer > highCardSecondPlayer)
                return string.Format("{0} wins - high card", firstPlayerName);
            return highCardFirstPlayer < highCardSecondPlayer ? string.Format("{0} wins - high card", secondPlayerName) : "Tie";
        }

        private string CheckForStraightFlush()
        {
            var firstStraightFlushValue = evaluator.StraightFlushValue(firstPlayerCards);
            var secondStraightFlushValue = evaluator.StraightFlushValue(secondPlayerCards);
            if (firstStraightFlushValue > secondStraightFlushValue)
                return (string.Format("{0} wins - straight flush", firstPlayerName));
            if (firstStraightFlushValue < secondStraightFlushValue)
                return (string.Format("{0} wins - straight flush", secondPlayerName));
            if (firstStraightFlushValue > Constants.NO_VALUE && secondStraightFlushValue > Constants.NO_VALUE)
                return "Tie";
            return Constants.NO_RESULT;
        }

        private string CheckFor4OfaKind()
        {
            var firstHandFourOfAKindValue = evaluator.FourOfAKindValue(firstPlayerCards);
            var secondHandFourOfAKindValue = evaluator.FourOfAKindValue(secondPlayerCards);
            if (firstHandFourOfAKindValue > secondHandFourOfAKindValue)
                return (string.Format("{0} wins - four of a kind", firstPlayerName));
            return secondHandFourOfAKindValue > firstHandFourOfAKindValue ?
                string.Format("{0} wins - four of a kind", secondPlayerName) : Constants.NO_RESULT;
        }

        private string CheckForAFullHouse()
        {
            var firstFullHouseRankValue = evaluator.FullHouseValue(firstPlayerCards);
            var secondFullHouseRankValue = evaluator.FullHouseValue(secondPlayerCards);
            if (firstFullHouseRankValue > secondFullHouseRankValue)
                return (string.Format("{0} wins - full house", firstPlayerName));
            return secondFullHouseRankValue > firstFullHouseRankValue ?
                string.Format("{0} wins - full house", secondPlayerName) : Constants.NO_RESULT;
        }

        private string CheckForAFlush()
        {
            var firstFlushRankValue = evaluator.FlushValue(firstPlayerCards);
            var secondFlushRankValue = evaluator.FlushValue(secondPlayerCards);
            if (firstFlushRankValue > secondFlushRankValue)
                return (string.Format("{0} wins - flush", firstPlayerName));
            if (firstFlushRankValue < secondFlushRankValue)
                return (string.Format("{0} wins - flush", secondPlayerName));
            return firstFlushRankValue != Constants.NO_VALUE ? "Tie" : Constants.NO_RESULT;
        }

        private string CheckForAStraight()
        {
            var firstStraightRankValue = evaluator.StraightValue(firstPlayerCards);
            var secondStraightRankValue = evaluator.StraightValue(secondPlayerCards);
            if (firstStraightRankValue > secondStraightRankValue)
                return (string.Format("{0} wins - straight", firstPlayerName));
            if (secondStraightRankValue > firstStraightRankValue)
                return (string.Format("{0} wins - straight", secondPlayerName));
            return firstStraightRankValue != Constants.NO_VALUE ? "Tie" : Constants.NO_RESULT;
        }

        private string CheckForTrips()
        {
            var firstTripsRankValue = evaluator.TripsValue(firstPlayerCards);
            var secondTripsRankValue = evaluator.TripsValue(secondPlayerCards);
            if (firstTripsRankValue > secondTripsRankValue)
                return (string.Format("{0} wins - trips", firstPlayerName));
            return firstTripsRankValue < secondTripsRankValue ? (string.Format("{0} wins - trips", secondPlayerName)) : Constants.NO_RESULT;
        }

        private string CheckForTwoPairs()
        {
            var firstTwoPairsValues = evaluator.TwoPairsValues(firstPlayerCards);
            var secondTwoPairsValues = evaluator.TwoPairsValues(secondPlayerCards);
            if (firstTwoPairsValues[TWOPAIR_LOWEST_PAIR] == Constants.NO_VALUE
                    && secondTwoPairsValues[TWOPAIR_LOWEST_PAIR] == Constants.NO_VALUE)
                return Constants.NO_RESULT;

            if (firstTwoPairsValues[TWOPAIR_HIGHEST_PAIR] > secondTwoPairsValues[TWOPAIR_HIGHEST_PAIR])
                return (string.Format("{0} wins - two pairs", firstPlayerName));
            if (firstTwoPairsValues[TWOPAIR_LOWEST_PAIR] < secondTwoPairsValues[TWOPAIR_LOWEST_PAIR])
                return (string.Format("{0} wins - two pairs", secondPlayerName));
            if (firstTwoPairsValues[TWOPAIR_KICKER] > secondTwoPairsValues[TWOPAIR_KICKER])
                return (string.Format("{0} wins - two pairs", firstPlayerName));
            return firstTwoPairsValues[TWOPAIR_KICKER] < secondTwoPairsValues[TWOPAIR_KICKER] ? (string.Format("{0} wins - two pairs", secondPlayerName)) : "Tie";
        }

        private string CheckForPair()
        {
            var firstHandPairRankValue = evaluator.PairValues(firstPlayerCards);
            var secondHandPairRankValue = evaluator.PairValues(secondPlayerCards);
            if (firstHandPairRankValue[0] == Constants.NO_VALUE && secondHandPairRankValue[0] == Constants.NO_VALUE)
                return Constants.NO_RESULT;

            for (var idx = 0; idx < firstHandPairRankValue.Length; idx++)
            {
                if (firstHandPairRankValue[idx] > secondHandPairRankValue[idx])
                    return (string.Format("{0} wins - pair", firstPlayerName));
                if (firstHandPairRankValue[idx] < secondHandPairRankValue[idx])
                    return (string.Format("{0} wins - pair", secondPlayerName));
            }
            return "Tie";
        }
    }
}