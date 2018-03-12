using System;
using DMCTS.GrainInterfaces;

namespace GoogleHashcode2018
{
    [Serializable]
    public class MakeRideAction : IAction
    {
        public Ride Ride { get; }
        public Car Car { get; }

        public MakeRideAction(Ride ride, Car car)
        {
            Ride = ride;
            Car = car;
        }

        public int GetScore(int bonus)
        {
            return GetScore(Ride, Car, bonus);
        }

        public static int GetScore(Ride ride, Car car, int bonus)
        {
            if (car.Equals(Car.SkipRide)) return 0;

            var score = 0;
            var rideDistance = ride.From.Distance(ride.To);
            var pickDistance = car.Location.Distance(ride.From);
            var waitTime = Math.Max(0, ride.Start - (car.Time + pickDistance));
            var timeToPick = car.Time + pickDistance;
            var onTime = timeToPick <= ride.Start;

            score += rideDistance - pickDistance - waitTime;
            if (onTime) score += bonus;

            return score;
        }

        public int GetFactScore(int bonus)
        {
            return GetFactScore(Ride, Car, bonus);
        }

        public static int GetFactScore(Ride ride, Car car, int bonus)
        {
            if (car.Equals(Car.SkipRide)) return 0;

            var score = 0;
            var rideDistance = ride.From.Distance(ride.To);
            var pickDistance = car.Location.Distance(ride.From);
            score += rideDistance;
            var timeToPick = car.Time + pickDistance;
            var onTime = timeToPick <= ride.Start;

            if (onTime) score += bonus;
            return score;
        }


        public override string ToString()
        {
            return $"{nameof(Ride)}: {Ride} \n {nameof(Car)}: {Car} \n Score: TODO \n FactScore: TODO";
        }

        protected bool Equals(MakeRideAction other)
        {
            return Ride.Equals(other.Ride) && Car.Equals(other.Car);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MakeRideAction)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Ride.GetHashCode() * 397) ^ Car.GetHashCode();
            }
        }
    }
}