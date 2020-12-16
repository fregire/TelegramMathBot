using Domain.Math;
using System;

namespace Domain.MathModule
{
    public class IntegralSolver
    {
        /// <summary>
        /// Returns definite integral value of current function
        /// </summary>
        /// <param name="function">Lambda-based function</param>
        /// <param name="upperBound">greater segment of curvilinear trapezoid</param>
        /// <param name="lowerBound">lower segment of curvilinear trapezoid</param>
        /// <returns>Integral value</returns>
        public static double Solve(DefiniteIntegral integral)
        {
            var result = 0d;
            var resSign = 1;

            if (integral.LowerBound > integral.UpperBound)
            {
                var tmp = integral.LowerBound;
                integral.LowerBound = integral.UpperBound;
                integral.UpperBound = tmp;
                resSign = -1;
            }

            for (var x = integral.LowerBound; x < integral.UpperBound; x += 10e-5) result += integral.Function(x) * 10e-5;
            return result * resSign;
        }
    }
}