using System.Collections.Generic;

namespace Valtech
{
    public interface ITownPlanner
    {
        IEnumerable<int> GetHouseNumbers();
        int GetTotalNumberOfHouse(IEnumerable<int> houseNumbers);
        IEnumerable<int> GetTheHousesOnNorthSide(IEnumerable<int> houseNumbers);
        IEnumerable<int> GetTheHousesOnSouthSide(IEnumerable<int> houseNumbers);
        Queue<House> GetHouseQueue(IEnumerable<int> houseNumbers);
    }
}