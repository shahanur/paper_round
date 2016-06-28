using System.Collections.Generic;
using NUnit.Framework;
using StoryQ;
using Valtech;

namespace PaperRound.Tests
{
    [TestFixture]
    public class NewsPaperDeliveryStory
    {
        private IEnumerable<int> _houseNumbers;
        private ITownPlanner _townPlanner;
        private IEnumerable<int> _deliveryOrder;
        private PaperBoyHelper _paperBoyHelper;
        private Util _utility;
        private Queue<House> _houseQueue;

        [SetUp]
        public void SetUp()
        {
            _utility = new Util();
            _townPlanner = new TownPlanner(_utility);
            _deliveryOrder = new List<int>();
            _houseQueue = new Queue<House>();
            _paperBoyHelper =  new PaperBoyHelper(_townPlanner);
            
        }

        [TearDown]
        public void TearDown()
        {
            _utility = null;
            _townPlanner = null;
            _paperBoyHelper = null;
            _deliveryOrder = null;
            _houseQueue = null;
        }

        #region AC5. (Approach 1) the list of house numbers in the order that I will be delivering to, so that I can sort by satchel in advance
        [Test]
        public void Given_AStreetSpecification_When_StartFromWestToEastAndEastToWest_Then_ItMatchesTheOrderOfHousesProvided()
        {
            var expectedNorthSideHouses = new List<int> {1,3,5,7,9,11,13,15,12,10,8,6,4,2};
            new Story("Delivering Newspaper")
                .InOrderTo("Sort my satchel in advance")
                .AsA("Paper boy")
                .IWant("The list of house numbers in order I will be delivering")
                .WithScenario("Travelling from west to east and starts on the north side of the street and then south side of the street delivering east to west")
                .Given(AStreetSpecification)
                .When(StartFromTheWestNorthSideOfTheRoad)
                .And(ChangeSide)
                .And(TravelBackFromEastToWest)
                .Then(VerifyTheOrderOfHousesAreEqual, expectedNorthSideHouses)
                .ExecuteWithReport();
        }
        #endregion

        #region AC5. (Approach 2) the list of house numbers in the order that I will be delivering to, so that I can sort by satchel in advance
        [Test]
        public void Given_AStreetSpecification_When_StartFromWestToEastForEachHouseInTheStree_Then_ItMatchesTheOrderOfHousesProvided()
        {
            var expectedNorthSideHouses = new List<int> { 1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15 };
            new Story("Delivering Newspaper")
                .InOrderTo("Sort my satchel in advance")
                .AsA("Paper boy")
                .IWant("The list of house numbers in order I will be delivering")
                .WithScenario("Travelling from west to east assuming start on the left (north) side of the road")
                .Given(AStreetSpecification)
                .When(StartFromTheWestToEast)
                .And(FindTheNextHouseAndCrossAccordinglyUntilReachesToTheLastNumber)
                .Then(VerifyTheOrderOfHousesAreEqual, expectedNorthSideHouses)
                .ExecuteWithReport();
        }
        #endregion

        #region AC6. (Approach 1) how many times I will have to cross the road from one side to the other to make my deliveries
        [Test]
        public void Given_AStreetSpecification_When_StartFromWestToEastAndEastToWest_Then_TotalCountOfCrossIs2()
        {
            new Story("Delivering Newspaper")
                .InOrderTo("Sort my satchel in advance")
                .AsA("Paper boy")
                .IWant("The list of house numbers in order I will be delivering")
                .WithScenario("Travelling from west to east and starts on the north side of the street and then south side of the street delivering east to west")
                .Given(AStreetSpecification)
                .When(StartFromTheWestNorthSideOfTheRoad)
                .And(ChangeSide)
                .And(TravelBackFromEastToWest)
                .Then(VerifyTheTotalCountOfCrossingEqual, 1)
                .ExecuteWithReport();
        }
        #endregion
        
        #region AC6. (Approach 2) how many times I will have to cross the road from one side to the other to make my deliveries
        [Test]
        public void Given_AStreetSpecification_When_StartFromWestToEastForEachHouseInTheStreet_Then_TotalCrossingCountIs8()
        {
            new Story("Delivering Newspaper")
                .InOrderTo("Sort my satchel in advance")
                .AsA("Paper boy")
                .IWant("The list of house numbers in order I will be delivering")
                .WithScenario("Travelling from west to east assuming start on the left (north) side of the road")
                .Given(AStreetSpecification)
                .When(StartFromTheWestToEast)
                .And(FindTheNextHouseAndCrossAccordinglyUntilReachesToTheLastNumber)
                .Then(VerifyTheTotalCountOfCrossingEqual, 8)
                .ExecuteWithReport();
        }
        #endregion

        #region Private Methods

        private void TravelBackFromEastToWest()
        {
            _deliveryOrder = _paperBoyHelper.GetDeliveryOrderOnSecondRoundEastToWest(_houseNumbers, _deliveryOrder);
        }
        private void FindTheNextHouseAndCrossAccordinglyUntilReachesToTheLastNumber()
        {
            _paperBoyHelper.DeliverNewsPaperFromWestToEast(Side.North, _deliveryOrder, _houseQueue);
        }

        private void StartFromTheWestNorthSideOfTheRoad()
        {
            _deliveryOrder = _paperBoyHelper.GetHouseNumbersOfNorthFromWestToEast(_houseNumbers);
        }

        private void StartFromTheWestToEast()
        {
            _houseQueue = _paperBoyHelper.GetHouseQueue(_houseNumbers);
        }

        private void VerifyTheTotalCountOfCrossingEqual(int expectedCrossing)
        {
            Assert.AreEqual(_paperBoyHelper.GetTotalCrossingCount(), expectedCrossing);
        }
        private void VerifyTheOrderOfHousesAreEqual(IEnumerable<int> expected)
        {
            CollectionAssert.AreEqual(_deliveryOrder, expected);
        }
        private void ChangeSide()
        {
            _paperBoyHelper.UpdateTotalCrossing();
        }

        private void AStreetSpecification()
        {
            _houseNumbers = _townPlanner.GetHouseNumbers();
        }

        #endregion
    }
}