using System;
using System.Data;

namespace Domain.MathModule
{
    public class ExpressionSolver
    {
        public static decimal Solve(string input)
        {
            var result = new DataTable().Compute(input, "");

            return Convert.ToDecimal(result);
        }
    }
}
