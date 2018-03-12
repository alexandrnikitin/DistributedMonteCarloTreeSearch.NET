using System;

namespace Rides
{
    [Serializable]
    public struct Car : IEquatable<Car>
    {
        public Car(int id, Point location, int time)
        {
            Id = id;
            Location = location;
            Time = time;
        }

        public int Id { get; }
        public Point Location { get; }
        public int Time { get; }
        public static Car SkipRide { get; } = new Car(-1, new Point(), -1);

        public bool CanMakeIt(Ride ride)
        {
            return ride.Finish >= Time + Location.Distance(ride.From) + ride.From.Distance(ride.To);
        }

        public bool CanMakeItOnTime(Ride ride)
        {
            var pickDistance = Location.Distance(ride.From);
            var timeToPick = Time + pickDistance;
            var onTime = timeToPick <= ride.Start;
            return onTime;
        }

        public bool Equals(Car other)
        {
            return Id == other.Id && Location.Equals(other.Location) && Time == other.Time;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Car && Equals((Car)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Location.GetHashCode();
                hashCode = (hashCode * 397) ^ Time;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Location)}: {Location}, {nameof(Time)}: {Time}";
        }
    }
}