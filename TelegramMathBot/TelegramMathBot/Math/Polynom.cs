using System;
using System.Collections.Generic;
using System.Linq;

namespace MathModule
{
    public class Polynom
    {
        private const double Epsilon = 10e-15;
        private const int scanStages = 10000 / 2;

        public List<double> Coefficients;

        public Polynom(List<double> coefficients)
        {
            var start = true;
            Coefficients = new List<double>();
            foreach (var coefficient in coefficients)
            {
                if (Math.Abs(coefficient) < Epsilon && start)
                    continue;
                start = false;
                Coefficients.Add(coefficient);
            }
        }

        public static List<double> GetDerivative(List<double> coefficients) => coefficients
            .Where(elem => true)
            .Reverse()
            .Select((elem, index) => elem * (index))
            .Where((elem, index) => index != 0)
            .Reverse()
            .ToList();

        public static List<double> FindRoots(List<double> coefficients)
        {
            var result = new List<double>();
            var degree = coefficients.Count;
            switch (degree)
            {
                case 0:
                    break;
                case 1:
                    result.Add(0);
                    break;
                case 2:
                    result.Add(-coefficients[1] / coefficients[0]);
                    break;
                case 3:
                    result.AddRange(QuadraticEquationSolver(coefficients[0], coefficients[1], coefficients[2]));
                    break;
                default:
                    var root = GetAnyPolynomRoot(coefficients);
                    result.Add(root);
                    coefficients = Divide(coefficients, root);
                    result.AddRange(FindRoots(coefficients));
                    break;
            }

            return result;
        }

        private static List<double> QuadraticEquationSolver(double a, double b, double c)
        {
            var result = new List<double>();
            var discriminant = b * b - 4 * a * c;
            if (Math.Abs(discriminant) < Epsilon)
                result.Add(-b / (2 * a));
            else if (discriminant > 0)
            {
                result.Add((-b - Math.Sqrt(discriminant)) / (2 * a));
                result.Add((-b + Math.Sqrt(discriminant)) / (2 * a));
            }

            return result;
        }

        public static List<double> RecursiveScan(List<double> coefficients, List<double> pointQueue, double scanBorders)
        {
            if (scanBorders < Epsilon)
            {
                return pointQueue;
            }

            var nextPointQueue = new List<double>();
            foreach (var point in pointQueue)
            {
                var prev = point - Epsilon;
                var prev2 = point - Epsilon * 2;
                var prevValue = GetPolynomValue(coefficients, prev);
                var prev2Value = GetPolynomValue(coefficients, prev2);

                for (var x = point - scanBorders; x < point + scanBorders; x += scanBorders / scanStages)
                {
                    var value = GetPolynomValue(coefficients, x);
                    if (Math.Abs(GetPolynomValue(coefficients, prev)) <=
                        Math.Abs(GetPolynomValue(coefficients, prev2)) &&
                        Math.Abs(GetPolynomValue(coefficients, prev)) <=
                        Math.Abs(GetPolynomValue(coefficients, value)))
                    {
                        nextPointQueue.Add(prev);
                    }

                    prev2 = prev;
                    prev = value;
                }
            }

            return RecursiveScan(coefficients, nextPointQueue, scanBorders * 10 / scanStages);
        }

        public static double GetAnyPolynomRoot1(List<double> coefficients)
        {
            var possibleRoots = RecursiveScan(coefficients, new List<double>(0), 100);

            throw new NotImplementedException();
        }

        public static double GetAnyPolynomRoot(List<double> coefficients)
        {
            var positiveX = 0d;
            var negativeX = 0d;
            var positiveFound = false;
            var negativeFound = false;
            var currentIntervalSize = 10d;
            while (!(positiveFound && negativeFound) && currentIntervalSize < 10e5)
            {
                for (var x = -currentIntervalSize; x < currentIntervalSize; x += currentIntervalSize / 10e3)
                {
                    if (GetPolynomValue(coefficients, x) > 0)
                    {
                        positiveFound = true;
                        positiveX = x;
                    }
                    else
                    {
                        negativeFound = true;
                        negativeX = x;
                    }

                    if (positiveFound && negativeFound)
                        break;
                }

                currentIntervalSize *= 11;
            }

            var middle = (positiveX + negativeX) / 2;
            while (Math.Abs(GetPolynomValue(coefficients, middle)) > Epsilon)
            {
                if (GetPolynomValue(coefficients, middle) > 0)
                    positiveX = middle;
                else
                    negativeX = middle;
                middle = (positiveX + negativeX) / 2;
            }

            return middle;
        }

        public static double GetPolynomValue(List<double> coefficients, double x)
        {
            var result = 0d;
            foreach (var d in coefficients)
            {
                result *= x;
                result += d;
            }

            return result;
        }

        public static List<double> Divide(List<double> coefficients, double value)
        {
            var accumulator = 0d;
            for (var i = 0; i < coefficients.Count; i++)
            {
                coefficients[i] += value * accumulator;
                accumulator = coefficients[i];
            }

            coefficients.RemoveAt(coefficients.Count - 1);
            return coefficients;
        }
    }
}