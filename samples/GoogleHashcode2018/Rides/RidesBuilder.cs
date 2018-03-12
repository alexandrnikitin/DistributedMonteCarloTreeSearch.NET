using System.Collections.Generic;

namespace Rides
{
    public class RidesBuilder
    {
        public static List<Ride> Build(string[] lines)
        {
            var rides = new List<Ride>(lines.Length);

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var pars = line.Split(' ');
                var a = int.Parse(pars[0]);
                var b = int.Parse(pars[1]);
                var x = int.Parse(pars[2]);
                var y = int.Parse(pars[3]);
                var s = int.Parse(pars[4]);
                var f = int.Parse(pars[5]);
                var ride = new Ride(i, new Point(a, b), new Point(x, y), s, f);
                rides.Add(ride);
            }

            return rides;
        }
    }
}