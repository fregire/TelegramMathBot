using System;
using System.Collections.Generic;
using System.Data;
using TelegramMathBot.Domain;
using TelegramMathBot.Infrastructure.GraphicModule;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View;
using TelegramMathBot.View.Parsers;
using TelegramMathBot.View.Commands;
using Telegram.Bot.Types;

namespace TelegramMathBot
{
    class Program
    {
        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        static void Main(string[] args)
        {
            var clientManager = new ClientManager();
            var bot = new TelegramBot(token);
            var mathBot = new MathBot(clientManager, GetCommands());
            var botSender = new BotSender(bot);

            bot.OnMessageTextReceived += (MessageTextEventArgs args) =>
                mathBot.ProcessMessage(args.Message);

            mathBot.OnReply += (ReplyEventArgs args) =>
                botSender.SendMessage(args.Response, args.ClientChat);

            bot.StartReceiving();

            Console.ReadLine();
        }

        static List<ICommand> GetCommands()
        {
            var expCommand = new ExpressionCommand();
            var helpCommand = new HelpCommand(new List<ICommand> { expCommand });

            return new List<ICommand> { expCommand, helpCommand };
        }
    }
}
