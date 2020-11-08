using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramMathBot.MathModule
{
    public class Polynom
    {
        private const double Epsilon = 10e-14;

        public static List<double> RecursiveScan(List<double> coefficients, List<double> pointQueue, double step)
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

        public static List<double> GetAllPolynomRoots(List<double> coefficients, double scanFrom, double scanTo, double step)
        {
            var pointQueue = new List<double>();
            for (var i = scanFrom; i < scanTo; i += step) 
                pointQueue.Add(i);
            var possibleRoots = RecursiveScan(coefficients, pointQueue, step);
            possibleRoots = ClearSingles(possibleRoots);
            possibleRoots = ClearDoubles(possibleRoots);
            return possibleRoots
                .Select(elem => Math.Round(elem, 10))
                .ToList();
        }

        public static List<double> ClearSingles(List<double> lst)
        {
            var result = new List<double>();
            for (var i = 1; i < lst.Count; i++)
                if (Math.Abs(lst[i] - lst[i-1]) < 10e-6) // magick
                    result.Add(lst[i]);
            return result;
        }

        public static List<double> ClearDoubles(List<double> lst)
        {
            var result = new List<double>{lst[0]};
            for (var i = 1; i < lst.Count; i++)
                if (Math.Abs(lst[i] - lst[i-1]) > Epsilon)
                    result.Add(lst[i]);
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