using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Parsers
{
    public class BoundsParser
    {
        public static (double LowerBound, double UpperBound) Parse(string input)
        {
            var args = ArgsParser.Parse(input, ",", 2);

            return (args[0], args[1]);
        }
    }
}
