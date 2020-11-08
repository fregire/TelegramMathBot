using System;
using System.Collections.Generic;

namespace TelegramMathBot
{
    class Program
    {
        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        static void Main(string[] args)
        {
            var bot = new TelegramBot(token);
            bot.Start();
        }
    }
}
