using System;
using System.Collections.Generic;

namespace TelegramMathBot.MathModule
{
    public class RealArgumentFunction
    {
        public Func<double, double> Body;
        public List<Tuple<double, double>> GraphPoints;

        private const double AppropriateInterval = 10e-4;

        public RealArgumentFunction(Func<double, double> body)
        {
            Body = body;
        }

        public void CalculatePointsInInterval(Tuple<double, double> xInterval, Tuple<double, double> yInterval)
        {
            GraphPoints = new List<Tuple<double, double>>();
            for (var x = xInterval.Item1; x < xInterval.Item2; x += AppropriateInterval)
            {
                var y = ValueAt(x);
                if (double.IsNaN(y)) continue;
                if (y > yInterval.Item1 && y < yInterval.Item2) GraphPoints.Add(new Tuple<double, double>(x, y));
            }
        }

        public double ValueAt(double x)
        {
            try
            {
                var result = Body(x);
                return result;
            }
            catch (Exception e)
            {
                return double.NaN;
            }
        }

        public RealArgumentFunction Derivative() =>
            new RealArgumentFunction((x) => 
                (Body(x) + Body(x + AppropriateInterval)) / AppropriateInterval);
    }
}