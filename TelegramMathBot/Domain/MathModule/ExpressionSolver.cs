using System.Data;

namespace Domain.MathModule
{
    public class ExpressionSolver
    {
        public static decimal Solve(string input)
        {
            return (decimal)new DataTable().Compute(input, "");
        }
    }
}
