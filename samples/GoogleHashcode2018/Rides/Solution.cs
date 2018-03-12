using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rides
{
    public class Solution
    {
        public List<List<MakeRideAction>> CarActions { get; set; }

        public Solution(int numberOfCars)
        {
            CarActions = new List<List<MakeRideAction>>(numberOfCars);
            for (var i = 0; i < numberOfCars; i++)
            {
                CarActions.Add(new List<MakeRideAction>());
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var v in CarActions)
            {
                sb.AppendLine($"{v.Count} {string.Join(" ", v.Select(x => x.Ride.Id))}");
            }

            return sb.ToString();
        }

        public List<int> GetMissedRides()
        {
            var rides = CarActions.SelectMany(x => x.Select(y => y.Ride.Id)).OrderBy(x => x).ToList();
            var result = Enumerable.Range(rides.Min(), rides.Count).Except(rides).ToList();
            return result;
        }

        public int GetTotalScore(int bonus)
        {
            var score = 0;
            foreach (var actions in CarActions.Where(x => x.Any()))
            {
                foreach (var action in actions)
                {
                    score += action.GetFactScore(Problem.BonusS);
                }
            }

            return score;
        }
    }
}