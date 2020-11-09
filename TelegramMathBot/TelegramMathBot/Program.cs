using System;
using System.Collections.Generic;
using TelegramMathBot.Infrastructure.GraphicModule;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View;

namespace TelegramMathBot
{
    class Program
    {
        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        // Данила, какого хуя ты хранишь токен в коде, притом в открытом репозитории?
        // Сынок ебаный, нельзя так делать.
        static void Main(string[] args)
        {
            GraphicSolver.Solve(
                200, 
                200,
                @"/home/fr1tzler/Desktop/Study/temp/test1", 
                new Tuple<double, double>(-5, 5),
                new Tuple<double, double>(-5, 5), 
                x => x * 2);
            //var bot = new TelegramBot(token);
            //bot.Start();
        }
    }
}
