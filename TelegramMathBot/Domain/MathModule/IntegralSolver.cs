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
        public static double Solve(Func<double, double> function, double upperBound, double lowerBound)
        {
            var result = 0d;
            for (var x = lowerBound; x < upperBound; x += 10e-5) result += function(x) * 10e-5;
            return result;
        }
    }
}