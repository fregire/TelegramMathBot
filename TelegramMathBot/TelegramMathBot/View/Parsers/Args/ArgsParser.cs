using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Parsers.Args
{
    public class ArgsParser
    {
        public static List<double> Parse(ArgsConfig config)
        {
            var result = new List<double>();
            var args = config.Input.Split(config.Delimeter);

            for(var i = 0; i < config.ArgsCount; i++)
                result.Add(double.Parse(args[i]));

            return result;
        }
    }
}
