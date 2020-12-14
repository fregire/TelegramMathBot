using System;
using System.Collections.Generic;
using System.Data;
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
using App;
using Ninject;
using System.Net.Http;
using TelegramMathBot.View.Commands.GraphicCommand;
using TelegramMathBot.View.Commands.ReferenceCommandF;
using TelegramMathBot.View.Commands.ExpressionCommand;
using TelegramMathBot.View.Commands.IntegralCommand;

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
            var container = ConfigureContainer();
            var bot = container.Get<TelegramBot>();
            var mathBot = container.Get<MathBot>();
            var botSender = container.Get<BotSender>();

            bot.SetCommands(mathBot.Commands);
            bot.OnMessageTextReceived += (MessageTextEventArgs args) =>
                mathBot.ProcessMessage(args.Message);

            mathBot.OnReply += (ReplyEventArgs args) =>
                botSender.SendMessage(args.Response, args.ClientChat);

            bot.StartReceiving();

            Console.ReadLine();
        }

        static StandardKernel ConfigureContainer()
        {
            var container = new StandardKernel();
            container.Bind<ImageFormat>().ToConstant(ImageFormat.Png);
            container.Bind<HttpClient>().ToConstant(new HttpClient());
            container.Bind<string>().ToConstant(token);
            container.Bind<ClientManager>().ToSelf().InSingletonScope();
            container.Bind<TelegramBotClient>().ToSelf().InSingletonScope();
            container.Bind<TelegramBot>().ToSelf().InSingletonScope();
            BindCommands(container);
            container.Bind<MathBot>().ToSelf().InSingletonScope();
            container.Bind<BotSender>().ToSelf().InSingletonScope();

            return container;
        }


        static void BindCommands(StandardKernel container)
        {
            container.Bind<ICommand>().To(typeof(GraphicHelp));
            container.Bind<ICommand>().To(typeof(RefHelp));
            container.Bind<ICommand>().To(typeof(IntegralHelp));
            container.Bind<ICommand>().To(typeof(ExpressionHelp));
            container
                .Bind<ICommand>()
                .To(typeof(StartCommand))
                .WhenInjectedInto<MathBot>();

            container
                .Bind<ICommand>()
                .To(typeof(HelpCommand))
                .WhenInjectedInto(typeof(MathBot));
        }
    }
}
