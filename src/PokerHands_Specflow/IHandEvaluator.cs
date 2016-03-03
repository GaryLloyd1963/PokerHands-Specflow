namespace PokerHands
{
    public interface IHandEvaluator
    {
        int StraightFlushValue(string hand);
        int FourOfAKindValue(string hand);
        int FullHouseValue(string hand);
        int FlushValue(string hand);
        int StraightValue(string hand);
        int TripsValue(string hand);
        int[] TwoPairsValues(string hand);
        int[] PairValues(string hand);
        int HighestRankAceIsHigh(string hand);
    }
}