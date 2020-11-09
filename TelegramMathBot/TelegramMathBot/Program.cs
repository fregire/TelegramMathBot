using System;
using System.Collections.Generic;
using TelegramMathBot.GraphicModule;
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
            var bot = new TelegramBot(token);
            bot.Start();
        }
    }
}
