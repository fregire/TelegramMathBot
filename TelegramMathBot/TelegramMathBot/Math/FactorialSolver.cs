using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegramMathBot.Math
{
    public class FactorialSolver
    {
        public static int Solve(int num)
        {
            return num == 0 ? 1 : Enumerable.Range(1, num).Aggregate((i, j) => i * j);
        }
    }
}
