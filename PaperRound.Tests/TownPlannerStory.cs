using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StoryQ;
using Valtech;

namespace PaperRound.Tests
{
    [TestFixture]
    public class TownPlannerStory
    {
        private IEnumerable<int> _houseNumbers;
        private TownPlanner _townPlanner;
        private bool _fileStartsFromOne;
        private int _totalNumberOfHouses;
        private int _totalHouseOnNorthSide;
        private int _totalHouseOnSouthSide;
        private Util _utility;

        [SetUp]
        public void SetUp()
        {
            _utility = new Util();
            _townPlanner = new TownPlanner(_utility);
        }

        [TearDown]
        public void TearDown()
        {
            _utility = null;
            _townPlanner = null;
        }

        private void AStreetSpecification()
        {
            _houseNumbers = _townPlanner.GetHouseNumbers();
        }

        #region AC1. that the file is valid.
        [Test]
        public void Given_AStreetFile_When_CheckNumber_Then_TheNumberStartsFromOne()
        {
            
            new Story("Layout of a street and numbering houses")
                .InOrderTo("Keep track of street layout and house numbering")
                .AsA("Town Planner")
                .IWant("To display a report")
                .WithScenario("House number starts from one")
                .Given(AStreetSpecification)
                .When(CheckNumberingStartsFromOne)
                .Then(VerifyTheStartingNumber, true)
                .Execute();
        }

        private void VerifyTheStartingNumber(bool expected)
        {
            Assert.IsTrue(_fileStartsFromOne == expected);
        }

        private void CheckNumberingStartsFromOne()
        {
            _fileStartsFromOne = _townPlanner.DoesNumberingStartsFromOne(_houseNumbers);
        }

        #endregion

        #region AC2. how many houses there are in a given street
        [Test]
        public void Given_AStreetFile_When_CountTotalNumberOfHouses_Then_TheTotalShouldBe14()
        {

            new Story("Layout of a street and numbering houses")
                .InOrderTo("Keep track of street layout and house numbering")
                .AsA("Town Planner")
                .IWant("To display a report")
                .WithScenario("Total number of houses in a given street")
                .Given(AStreetSpecification)
                .When(CountTotalNumberOfHouses)
                .Then(VerifyTheTotalCount, 14)
                .Execute();
        }

        private void VerifyTheTotalCount(int expectedCount)
        {
            Assert.AreEqual(_totalNumberOfHouses,expectedCount);
        }

        private void CountTotalNumberOfHouses()
        {
            _totalNumberOfHouses = _townPlanner.GetTotalNumberOfHouse(_houseNumbers);
        }

        #endregion

        #region AC3. how many houses are on the left hand (north) side of the street
        [Test]
        public void Given_AStreetFile_When_CountTotalHousesOfNorthSide_Then_TheTotalShouldBe8()
        {

            new Story("Layout of a street and numbering houses")
                .InOrderTo("Keep track of street layout and house numbering")
                .AsA("Town Planner")
                .IWant("To display a report")
                .WithScenario("Total number on the left hand (north) side of the street")
                .Given(AStreetSpecification)
                .When(CountTotalHousesOfNorthSide)
                .Then(VerifyTheTotalCountOfNorthSide, 8)
                .Execute();
        }

        private void VerifyTheTotalCountOfNorthSide(int count)
        {
            Assert.AreEqual(_totalHouseOnNorthSide,count);
        }

        private void CountTotalHousesOfNorthSide()
        {
            _totalHouseOnNorthSide = _townPlanner.GetTheHousesOnNorthSide(_houseNumbers).Count();
        }
        #endregion 

        #region AC4. how many houses are on the right hand (south) side of the street
        [Test]
        public void Given_AStreetFile_When_CountTotalHousesOfSouthSide_Then_TheTotalShouldBe6()
        {

            new Story("Layout of a street and numbering houses")
                .InOrderTo("Keep track of street layout and house numbering")
                .AsA("Town Planner")
                .IWant("To display a report")
                .WithScenario("Total number on the left hand (north) side of the street")
                .Given(AStreetSpecification)
                .When(CountTotalHousesOfSouthSide)
                .Then(VerifyTheTotalCountOfSouthSide, 6)
                .Execute();
        }

        private void VerifyTheTotalCountOfSouthSide(int count)
        {
            Assert.AreEqual(_totalHouseOnSouthSide, count);
        }

        private void CountTotalHousesOfSouthSide()
        {
            _totalHouseOnSouthSide = _townPlanner.GetTheHousesOnSouthSide(_houseNumbers).Count();
        }
        #endregion 
    }
}
