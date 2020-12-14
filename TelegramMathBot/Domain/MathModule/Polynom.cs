using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.MathModule
{
    public static class Polynom
    {
        private const double Epsilon = 10e-14;
        
        /// <summary>
        /// Finds roots of polynom with real coefficients;
        /// </summary>
        /// <param name="coefficients">List of coefficients, from greater degree to lower</param>
        /// <param name="scanFrom">Start of the segment, where scan will be done</param>
        /// <param name="scanTo">End of the segment, where scan will be done</param>
        /// <param name="step">Distance between scanning points on first iteration</param>
        /// <returns>List containing every root once, regardless of the multiplicity</returns>
        public static List<double> GetAllPolynomRoots(List<double> coefficients, double scanFrom, double scanTo, double step)
        {
            var resultRoots = new List<double>();
            var pointQueue = new List<double>();
            for (var i = scanFrom; i < scanTo; i += step) 
                pointQueue.Add(i);
            var possibleRoots = RecursiveScan(coefficients, pointQueue, step);
            possibleRoots = ClearSingles(possibleRoots);
            possibleRoots = ClearDoubles(possibleRoots);

            foreach(var root in possibleRoots)
            {
                var res = GetResult(coefficients, root);
                if (res < 10e-6)
                    resultRoots.Add(Math.Round(root, 10));
            }

            return resultRoots;
        }

        private static double GetResult(List<double> coefficients, double value)
        {
            var coeffsCount = coefficients.Count;
            var summ = 0.0;

            for(var i = coeffsCount - 1; i >= 0; i--)
                summ += coefficients[i] * Math.Pow(value, coeffsCount - i - 1);

            return summ;
        }

        private static List<double> RecursiveScan(List<double> coefficients, List<double> pointQueue, double step)
        {
            if (step < Epsilon) 
                return pointQueue;
            var nextPointQueue = new List<double>();
            for(var i = 0; i < pointQueue.Count; i++)
            {
                var elem = pointQueue[i];
                var possibleNext = elem - step;
                while (Math.Abs(GetPolynomValue(coefficients, elem)) >
                       Math.Abs(GetPolynomValue(coefficients, possibleNext)))
                {
                    elem -= step;
                    possibleNext -= step;
                }
                nextPointQueue.Add(elem);
            }
            return RecursiveScan(coefficients, nextPointQueue, step / 2);
        }
        
        private static List<double> ClearSingles(List<double> argumentList)
        {
            var result = new List<double>();
            for (var i = 1; i < argumentList.Count; i++)
                if (Math.Abs(argumentList[i] - argumentList[i-1]) < 10e-6) // magick
                    result.Add(argumentList[i]);
            return result;
        }

        private static List<double> ClearDoubles(List<double> argumentList)
        {
            var result = new List<double>{argumentList[0]};
            for (var i = 1; i < argumentList.Count; i++)
                if (Math.Abs(argumentList[i] - argumentList[i-1]) > Epsilon)
                    result.Add(argumentList[i]);
            return result;
        }
        
        private static double GetPolynomValue(List<double> coefficients, double x)
        {
            var result = 0d;
            foreach (var d in coefficients)
                result = result * x + d;
            return result;
        }
    }
}