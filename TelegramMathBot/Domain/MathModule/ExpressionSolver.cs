using System;
using System.Data;

namespace Domain.MathModule
{
    public class ExpressionSolver: ISolver<string, decimal>
    {
        public decimal Solve(string input)
        {
            var result = new DataTable().Compute(input, "");

            return Convert.ToDecimal(result);
        }
    }
}
