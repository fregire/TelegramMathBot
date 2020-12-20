using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Parsers.Args
{
    public class ArgsConfig
    {
        public string Input;
        public string Delimeter;
        public int ArgsCount;

        public ArgsConfig(string input, string delimeter, int argsCount)
        {
            Input = input;
            Delimeter = delimeter;
            ArgsCount = argsCount;
        }
    }
}
