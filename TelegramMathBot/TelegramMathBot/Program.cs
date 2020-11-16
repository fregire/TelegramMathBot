using System;
using System.Collections.Generic;
using System.Data;
using TelegramMathBot.Domain;
using TelegramMathBot.Infrastructure.GraphicModule;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View;
using TelegramMathBot.View.Parsers;
using TelegramMathBot.View.Commands;

namespace TelegramMathBot
{
    class Program
    {
        /*
            В нашем боте -> распарсенные данные для app
            bot.OnMessage += app.ProcessMessage;
            app.OnReply += bot.ProcessReply;
            */
        // ClientManager вместо app только для работы с клиентами

        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        static void Main(string[] args)
        {
            var app = new App();
            var expCommand = new ExpressionCommand();
            var helpCommand = new HelpCommand(new List<ICommand> { expCommand});
            var bot = new TelegramBot(token);
            var mathBot = new MathBot(bot, app, new List<ICommand> { expCommand, helpCommand});
            mathBot.Start();
            Console.ReadLine();
        }
    }
}
