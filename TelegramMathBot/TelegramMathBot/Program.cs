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
using TelegramMathBot.View.ImageFormats;
using Domain.MathModule;
using Domain.MathModule.Graphic;
using Domain.AdditionalMath;

namespace TelegramMathBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
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
        }

        static StandardKernel ConfigureContainer()
        {
            var container = new StandardKernel();
            BindImageFormats(container);
            BindSolversAndParsers(container);
            BindCommands(container);
            container
                .Bind<HttpClient>()
                .ToConstant(new HttpClient())
                .WhenInjectedInto<TelegramBotClient>();
            container
                .Bind<string>()
                .ToConstant(Properties.Resources.Config)
                .WhenInjectedInto<TelegramBotClient>();
            container.Bind<ClientManager>().ToSelf().InSingletonScope();
            container.Bind<TelegramBotClient>().ToSelf().InSingletonScope();
            container.Bind<TelegramBot>().ToSelf().InSingletonScope();
            container.Bind<MathBot>().ToSelf().InSingletonScope();
            container.Bind<BotSender>().ToSelf().InSingletonScope();

            return container;
        }


        static void BindCommands(StandardKernel container)
        {
            container.Bind<ICommand>().To<GraphicHelp>();
            container.Bind<ICommand>().To<RefHelp>();
            container.Bind<ICommand>().To<IntegralHelp>();
            container.Bind<ICommand>().To<ExpressionHelp>();
            container
                .Bind<ICommand>()
                .To<StartCommand>()
                .WhenInjectedInto<MathBot>();

            container
                .Bind<ICommand>()
                .To<HelpCommand>()
                .WhenInjectedInto<MathBot>();
        }

        static void BindSolversAndParsers(StandardKernel container)
        {
            container
                .Bind<ISolver<string, decimal>>()
                .To<ExpressionSolver>()
                .WhenInjectedInto<ExpressionHelp>();
            container
                .Bind<IParser<string, string>>()
                .To<ExpressionParser>()
                .WhenInjectedInto<ExpressionHelp>();

            container
                .Bind<ISolver<GraphicConfig, SFML.Graphics.Image>>()
                .To<GraphicSolver>()
                .WhenInjectedInto<GraphicHelp>();
            container
                .Bind<IParser<string, Func<double, double>>>()
                .To<FunctionParser>()
                .WhenInjectedInto<GraphicHelp>();

            container
                .Bind<ISolver<DefiniteIntegral, double>>()
                .To<IntegralSolver>()
                .WhenInjectedInto<IntegralHelp>();
            container
                .Bind<IParser<string, Func<double, double>>>()
                .To<FunctionParser>()
                .WhenInjectedInto<IntegralHelp>();
            container
               .Bind<IParser<string, (double LowerBound, double UpperBound)>>()
               .To<BoundsParser>()
               .WhenInjectedInto<IntegralHelp>();
        }

        static void BindImageFormats(StandardKernel container)
        {
            container.Bind<IImageFormat>().To<PNGImageFormat>();
            container.Bind<IImageFormat>().To<ASCIIImageFormat>();
        }
    }
}
