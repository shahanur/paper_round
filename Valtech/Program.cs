using System;
using System.Collections.Generic;
using System.Linq;

namespace Valtech
{
    class Program
    {
        static void Main(string[] args)
        {
            var townPlanner = new TownPlanner(new Util());
            var houseNumbers = townPlanner.GetHouseNumbers();

            // story 1
            Console.WriteLine("Story 1");
            DisplayTownPlannerReport(townPlanner,houseNumbers);

            // story 2: 
            Console.WriteLine();
            Console.WriteLine("Story 2");
            
            //approach 1
            ShowTheSuggessionToThePaperBoyOnApprachOne(new PaperBoyHelper(townPlanner),houseNumbers);

            //approach 2
            ShowTheSuggessionToThePaperBoyOnApprachTwo(new PaperBoyHelper(townPlanner), houseNumbers);

            Console.ReadKey();
        }

        private static void DisplayTownPlannerReport(TownPlanner townPlanner, IEnumerable<int> houseNumbers)
        {   
            var isFileValid = townPlanner.DoesNumberingStartsFromOne(houseNumbers);
            Console.WriteLine("The file is {0}", isFileValid?"valid":"not valid");

            var totalHouses = townPlanner.GetTotalNumberOfHouse(houseNumbers);
            Console.WriteLine("Total houses in the given street are {0}",totalHouses);

            var totalHousesOnNorth = townPlanner.GetTheHousesOnNorthSide(houseNumbers).Count();
            Console.WriteLine("Total houses on the north side of the street are {0}", totalHousesOnNorth);

            var totalHousesOnSouth = townPlanner.GetTheHousesOnSouthSide(houseNumbers).Count();
            Console.WriteLine("Total houses on the south side of the street are {0}", totalHousesOnSouth);
        }

        private static void ShowTheSuggessionToThePaperBoyOnApprachOne(PaperBoyHelper paperBoyHelper, IEnumerable<int> houseNumbers)
        {
            
            var deliveryOrderInApproachOne = paperBoyHelper.GetHouseNumbersOfNorthFromWestToEast(houseNumbers);
            paperBoyHelper.UpdateTotalCrossing();
            deliveryOrderInApproachOne = paperBoyHelper.GetDeliveryOrderOnSecondRoundEastToWest(houseNumbers, deliveryOrderInApproachOne);

            Console.WriteLine("Approach 1: Houses to deliver in order are {0}",string.Join(",", deliveryOrderInApproachOne));
            Console.WriteLine("Approach 1: Total crossing time is: {0}",paperBoyHelper.GetTotalCrossingCount());

        }

        private static void ShowTheSuggessionToThePaperBoyOnApprachTwo(PaperBoyHelper paperBoyHelper, IEnumerable<int> houseNumbers)
        {
            var deliveryOrderInApproachTwo = new List<int>();
               var houseQueue = paperBoyHelper.GetHouseQueue(houseNumbers);
            paperBoyHelper.DeliverNewsPaperFromWestToEast(Side.North, deliveryOrderInApproachTwo, houseQueue);

            Console.WriteLine();
            Console.WriteLine("Approach 2: Houses to deliver in order are {0}", string.Join(",", deliveryOrderInApproachTwo));
            Console.WriteLine("Approach 2: Total crossing time is: {0}", paperBoyHelper.GetTotalCrossingCount());

        }
    }
}
