using Domain.AdditionalMath;
using System.Collections.Generic;

namespace Domain.MathModule
{
    public class PolynomialSolver: ISolver<List<double>, List<double>>
    {
        /// <summary>
        /// Solves polynom equations
        /// </summary>
        /// <param name="coefficients">List of coefficients, from greater to lower degree</param>
        /// <returns>List of uniqur polynomial roots</returns>
        public List<double> Solve(List<double> coefficients) =>
            Polynom.GetAllPolynomRoots(coefficients, -100, 100, 0.1);

    }
}