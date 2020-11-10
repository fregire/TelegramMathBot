using System;
using System.Collections.Generic;
using System.Data;
using TelegramMathBot.Domain;
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
            var expCommand = new Command(
                RequestType.Expression, 
                true, 
                (message) =>
                {
                    var data = ExpressionParser.Parse(message);
                    return ExpressionSolver.Solve(data).ToString();
                },
                "/exp",
                "Введите выражение");
            
            var helpCommand = new Command(
                RequestType.Help,
                false,
                (message) =>
                {
                    var commands = new List<Command> { expCommand };
                    // Make help from commands....
                    return "";
                },
                "/exp",
                "Введите выражение");
            // ClientManager вместо app только для работы с клиентами
            var app = new App();
            var bot = new TelegramBot(token, app);

            /*
            В нашем боте -> распарсенные данные для app
            bot.OnMessage += app.ProcessMessage;
            app.OnReply += bot.ProcessReply;
            */
            bot.Start();
        }
    }
}
