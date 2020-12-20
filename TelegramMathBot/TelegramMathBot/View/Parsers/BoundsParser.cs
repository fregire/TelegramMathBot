using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Parsers.Args;

namespace TelegramMathBot.View.Parsers
{
    public class BoundsParser: 
        IParser<string, (double LowerBound, double UpperBound)>
    {
        public (double LowerBound, double UpperBound) Parse(string input)
        {
            var config = new ArgsConfig(input, ",", 2);
            var args = ArgsParser.Parse(config);

            return (args[0], args[1]);
        }
    }
}
