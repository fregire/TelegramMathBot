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
        public static double Solve(DefiniteIntegral intergral)
        {
            var result = 0d;
            for (var x = intergral.LowerBound; x < intergral.UpperBound; x += 10e-5) result += intergral.Function(x) * 10e-5;
            return result;
        }
    }
}