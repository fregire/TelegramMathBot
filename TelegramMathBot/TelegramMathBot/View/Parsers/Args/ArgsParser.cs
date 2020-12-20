using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Parsers
{
    public class ArgsParser
    {
        public static List<double> Parse(string input, string delimeter, int argsCount)
        {
            var result = new List<double>();
            var args = input.Split(delimeter);

            for(var i = 0; i < argsCount; i++)
                result.Add(double.Parse(args[i]));

            return result;
        }
    }
}
