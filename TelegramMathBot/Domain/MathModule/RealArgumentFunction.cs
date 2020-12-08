using System;
using System.Collections.Generic;

namespace Domain.MathModule
{
    public class RealArgumentFunction
    {
        private readonly Func<double, double> _body;
        public List<Tuple<double, double>> GraphPoints;

        private const double AppropriateInterval = 10e-4;

        public RealArgumentFunction(Func<double, double> body)
        {
            _body = body;
        }

        /// <summary>
        /// Fills list GraphPoints with all points belonging to the function on the specified rectangular set;
        /// </summary>
        /// <param name="xInterval">Horisontal segment that characterizes the set</param>
        /// <param name="yInterval">Vertical segment that characterizes the set</param>
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

        private double ValueAt(double x)
        {
            try
            {
                var result = _body(x);
                return result;
            }
            catch
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Returns function derivative, based on limit derivative finding method
        /// </summary>
        /// <returns>New function, that's equal to current function derivative on its domain</returns>
        public RealArgumentFunction Derivative() =>
            new RealArgumentFunction((x) =>
                (_body(x) + _body(x + AppropriateInterval)) / AppropriateInterval);
    }
}