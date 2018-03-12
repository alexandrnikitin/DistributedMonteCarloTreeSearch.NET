using System.Collections.Generic;
using System.Linq;

namespace Rides
{
    public class ProblemBuilder
    {
        public static Problem Build(string[] lines)
        {
            var pars = lines[0].Split(' ');
            var R = int.Parse(pars[0]);
            var C = int.Parse(pars[1]);
            var F = int.Parse(pars[2]);
            var N = int.Parse(pars[3]);
            var B = int.Parse(pars[4]);
            var T = int.Parse(pars[5]);

            var rides = RidesBuilder.Build(lines.Skip(1).ToArray());

            var cars = new List<Car>(F);
            for (var i = 0; i < F; i++)
            {
                var car = new Car(i, new Point(), 0);
                cars.Add(car);
            }

            Problem.BonusS = B;
            return new Problem(R, C, F, N, B, T, rides, cars);
        }
    }
}