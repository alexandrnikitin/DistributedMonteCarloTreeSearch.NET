using System;
using System.Collections.Generic;
using System.Linq;
using Orleans.Concurrency;

namespace Rides
{
    [Serializable]
    [Immutable]
    public class RidesView3
    {
        public int? LastRemoved { get; set; }
        public List<Ride> EarliestFinish { get; set; }
        public int Bonus { get; set; }

        public RidesView3(IEnumerable<Ride> rides, int bonus)
        {
            Bonus = bonus;
            EarliestFinish = rides.OrderBy(x => x, EarliestComparer).ToList();
        }

        private RidesView3(List<Ride> earliestFinish, int? lastRemoved, int bonus)
        {
            EarliestFinish = earliestFinish;
            Bonus = bonus;
            LastRemoved = lastRemoved;
        }

        public Ride GetEarliestFinish()
        {
            if (LastRemoved.HasValue)
            {
                if (LastRemoved.Value + 1 < EarliestFinish.Count)
                {
                    return EarliestFinish[LastRemoved.Value + 1];
                }
                else
                {
                    return default;
                }
            }
            else
                return EarliestFinish[0];
        }

        public RidesView3 Remove(Ride ride)
        {
            return LastRemoved.HasValue
                ? new RidesView3(EarliestFinish, LastRemoved.Value + 1, Bonus)
                : new RidesView3(EarliestFinish, 0, Bonus);
        }

        public int Count => LastRemoved.HasValue ? EarliestFinish.Count - LastRemoved.Value : EarliestFinish.Count - 1;

        private sealed class EarliestRelationalComparer : IComparer<Ride>
        {
            public int Compare(Ride x, Ride y)
            {
                var finishX = x.Start + x.From.Distance(x.To);
                var finishY = y.Start + y.From.Distance(y.To);
                return finishX == finishY ? x.Id.CompareTo(y.Id) : finishX.CompareTo(finishY);
            }
        }

        public static IComparer<Ride> EarliestComparer { get; } = new EarliestRelationalComparer();

    }
}