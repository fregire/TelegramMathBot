using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TelegramMathBot.MathModule;

namespace TelegramMathBot.Parsers
{
    public class ExpressionParser
    {
        public static string Parse(string input)
        {
            var factorials = new Regex("[0-9]+!");
            var matches = factorials.Matches(input);

            if (matches.Count == 0)
                return input;

            foreach(Match match in matches)
            {
                var val = match.Value;
                var num = int.Parse(val.Substring(0, val.Length - 1));
                var result = FactorialSolver.Solve(num);
                input = input.Replace(val, result.ToString());
            }

            return input;
        }
    }
}
