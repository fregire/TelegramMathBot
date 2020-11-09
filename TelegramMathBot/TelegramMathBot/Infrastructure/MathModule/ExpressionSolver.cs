using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TelegramMathBot.MathModule
{
    public class ExpressionSolver
    {
        public static int Solve(string input)
        {
            return (int) new DataTable().Compute(input, "");
        }
    }
}
