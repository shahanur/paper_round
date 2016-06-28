using System.Collections.Generic;
using System.Linq;

namespace Valtech
{
    public class PaperBoyHelper
    {
        private readonly ITownPlanner _townPlanner;
        private int _totalCrossCount;

        public PaperBoyHelper(ITownPlanner townPlanner)
        {
            _townPlanner = townPlanner;
        }

        public IEnumerable<int> GetHouseNumbersOfNorthFromWestToEast(IEnumerable<int> houseNumbers )
        {
            return _townPlanner.GetTheHousesOnNorthSide(houseNumbers);
        }

        public IEnumerable<int> GetHouseNumbersOfSouthFromEastToWest(IEnumerable<int> houseNumbers)
        {
            return _townPlanner.GetTheHousesOnSouthSide(houseNumbers).OrderByDescending(n=>n);
        }

        public IEnumerable<int> GetDeliveryOrderOnSecondRoundEastToWest(IEnumerable<int> houseNumbers,IEnumerable<int> deliveryOrder)
        {
            return deliveryOrder.Concat(GetHouseNumbersOfSouthFromEastToWest(houseNumbers));
        }

        public Queue<House> GetHouseQueue(IEnumerable<int> houseNumbers)
        {
            return _townPlanner.GetHouseQueue(houseNumbers);
        }

        public void UpdateTotalCrossing()
        {
            _totalCrossCount++;
        }

        public int GetTotalCrossingCount()
        {
            return _totalCrossCount;
        }

        public void DeliverNewsPaperFromWestToEast(Side currentSide,IEnumerable<int> deliveryOrder, Queue<House> houseQueue  )
        {
            
            var house = houseQueue.Dequeue();
            if (house != null)
            {
                (deliveryOrder as List<int>).Add(house.Number);

                if (currentSide != house.HouseSide)
                {
                    UpdateTotalCrossing();
                    currentSide = house.HouseSide;
                }
            }
            if (houseQueue.Count > 0)
                DeliverNewsPaperFromWestToEast(currentSide, deliveryOrder,houseQueue);
        }
    }
}