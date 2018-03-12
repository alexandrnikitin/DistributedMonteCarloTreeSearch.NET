using System;
using System.Collections.Generic;

namespace GoogleHashcode2018
{
    [Serializable]
    public struct Ride : IEquatable<Ride>
    {
        public int Id { get; }
        public Point From { get; }
        public Point To { get; }
        public int Start { get; }
        public int Finish { get; }

        public Ride(int id, Point @from, Point to, int start, int finish)
        {
            Id = id;
            From = @from;
            To = to;
            Start = start;
            Finish = finish;
        }

        public bool Equals(Ride other)
        {
            return Id == other.Id && From.Equals(other.From) && To.Equals(other.To) && Start == other.Start && Finish == other.Finish;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Ride && Equals((Ride) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ From.GetHashCode();
                hashCode = (hashCode * 397) ^ To.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ Finish;
                return hashCode;
            }
        }

        private sealed class FinishRelationalComparer : IComparer<Ride>
        {
            public int Compare(Ride x, Ride y)
            {
                return x.Finish.CompareTo(y.Finish);
            }
        }

        public static IComparer<Ride> FinishComparer { get; } = new FinishRelationalComparer();

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(From)}: {From}, {nameof(To)}: {To}, {nameof(Start)}: {Start}, {nameof(Finish)}: {Finish}, Distance: {From.Distance(To)}";
        }

    }
}