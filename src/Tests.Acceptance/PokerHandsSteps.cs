using NUnit.Framework;
using PokerHands;
using TechTalk.SpecFlow;

namespace Tests.Acceptance
{
    [Binding]
    public class PokerHandsSteps
    {
        private string _actualResult;
        private string _blackhand;
        private string _whiteHand;

        [Given(@"the hand dealt to Black is '(.*)'")]
        public void GivenTheHandDealtToBlackIs(string cards)
        {
            _blackhand = cards;
        }

        [Given(@"the hand dealt to White is  '(.*)'")]
        public void GivenTheHandDealtToWhiteIs(string cards)
        {
            _whiteHand = cards;
        }

        [When(@"I compare the hands")]
        public void WhenICompareTheHands()
        {
            var comparer = new PokerHandsComparer();
            _actualResult = comparer.CompareHands("Black", _blackhand, "White", _whiteHand);
        }

        [Then(@"the comparison result is '(.*)'")]
        public void ThenTheComparisonResultIs(string expectedResult)
        {
            Assert.That(_actualResult, Is.EqualTo(expectedResult));
        }

    }
}
