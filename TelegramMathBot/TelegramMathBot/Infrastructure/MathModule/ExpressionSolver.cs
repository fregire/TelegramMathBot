using System.Data;

namespace TelegramMathBot.Infrastructure.MathModule
{
    public class ExpressionSolver
    {
        public static int Solve(string input)
        {
            return (int) new DataTable().Compute(input, "");
        }
    }
}
