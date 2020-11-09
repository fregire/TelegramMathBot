using System;
using System.Collections.Generic;
using System.Data;
using TelegramMathBot.Infrastructure.GraphicModule;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot
{
    class Program
    {
        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        // Данила, какого *удалено* ты хранишь токен в коде, притом в открытом репозитории?
        // Сынок *удалено*, нельзя так делать *удалено*
        static void Main(string[] args)
        {
            var bot = new TelegramBot(token);
            bot.Start();
        }
    }
}
