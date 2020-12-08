using System.Linq;

namespace Domain.MathModule
{
    public class FactorialSolver
    {
        public static int Solve(int num)
        {
            return num == 0 ? 1 : Enumerable.Range(1, num).Aggregate((i, j) => i * j);
        }
    }
}
