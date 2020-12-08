using System;
using System.Collections.Generic;
using System.Data;
using TelegramMathBot.App;
using TelegramMathBot.Domain.GraphicModule;
using TelegramMathBot.Domain.MathModule;
using TelegramMathBot.View;
using TelegramMathBot.View.Parsers;
using TelegramMathBot.View.Commands;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.IO;
using Telegram.Bot.Types.InputFiles;
using SFML.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using Telegram.Bot.Types.Enums;
using System.Reflection;
using System.Threading;

namespace TelegramMathBot
{
    class Program
    {
        //Config file и добавитьь в .gitignore
        //431331940 
        static string token = "1495097120:AAHpmNmtzpgF6-_BZe0yXyGdfQYrUdhMokQ";
        static void Main(string[] args)
        {
            Start();
            Console.ReadLine();
        }

        static void Start()
        {
            var clientManager = new ClientManager();
            var bot = new TelegramBot(new TelegramBotClient(token));
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
            var graphicCommand = CommandsFactory.CreateGraphicCommand(ImageFormat.Png);
            var referenceCommand = CommandsFactory.CreateReferenceCommand();
            var expCommand = CommandsFactory.CreateExpressionCommand();
            var helpCommand = CommandsFactory.CreateHelpCommand(
                new List<ICommand> { expCommand, graphicCommand, referenceCommand });

            return new List<ICommand> { expCommand, helpCommand, graphicCommand, referenceCommand };
        }
    }
}
