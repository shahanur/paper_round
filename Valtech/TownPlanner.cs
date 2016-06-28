using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Valtech
{
    public class TownPlanner : ITownPlanner
    {
        private readonly Util _utility;

        public TownPlanner(Util utility)
        {
            _utility = utility;
        }

        private const string StreetFile = "street1.txt";
        public bool DoesNumberingStartsFromOne(IEnumerable<int> houseNumbers ) {
            return houseNumbers.First() == 1; 
        }

        public IEnumerable<int> GetHouseNumbers()
        {
            string numberText;
            using (var file = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StreetFile)))
            {
                numberText = file.ReadToEnd();
            }
            return numberText.Split(" ".ToCharArray())
                .Where(number => !string.IsNullOrEmpty(number))
                .Select(number => Convert.ToInt32(number));
        }

        public int GetTotalNumberOfHouse(IEnumerable<int> houseNumbers)
        {
            return houseNumbers.Count();
        }

        public IEnumerable<int> GetTheHousesOnNorthSide(IEnumerable<int> houseNumbers)
        {
            return houseNumbers.Where(_utility.IsLocatedOnNorth).Select(houseNumber =>houseNumber);
        }

        public IEnumerable<int> GetTheHousesOnSouthSide(IEnumerable<int> houseNumbers)
        {
            return houseNumbers.Where(houseNumber => !_utility.IsLocatedOnNorth(houseNumber)).Select(houseNumber => houseNumber);
        }

        public Queue<House> GetHouseQueue(IEnumerable<int> houseNumbers)
        {
            var houseQueue = new Queue<House>();
            foreach (var number in houseNumbers)
            {
                houseQueue.Enqueue(new House
                {
                    Number = number,
                    HouseSide = _utility.IsLocatedOnNorth(number) ? Side.North : Side.South
                });
            }

            return houseQueue;
        }


    }
}