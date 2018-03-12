using System;
using System.Collections.Generic;

namespace Rides
{
    [Serializable]
    public class Problem
    {
        public int Rows { get; }
        public int Columns { get; }
        public int NumberOfCars { get; }
        public int NumberOfRides { get; }
        public int Bonus { get; }
        public int TotalTime { get; }
        public List<Ride> Rides { get; }
        public List<Car> Cars { get; }

        public static int BonusS { get; set; }

        public Problem(
            int rows, int columns,
            int numberOfCars, int numberOfRides,
            int bonus,
            int totalTime,
            List<Ride> rides,
            List<Car> cars)
        {
            Rows = rows;
            Columns = columns;
            NumberOfCars = numberOfCars;
            NumberOfRides = numberOfRides;
            Bonus = bonus;
            TotalTime = totalTime;
            Rides = rides;
            Cars = cars;
        }
    }
}