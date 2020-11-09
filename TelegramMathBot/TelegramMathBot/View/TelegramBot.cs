using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramMathBot.View.Parsers;
using TelegramMathBot.Domain;
using Telegram.Bot.Types.InputFiles;
using System.IO;
using System.Linq;

namespace TelegramMathBot.View
{
    public class TelegramBot
    {
        private readonly string token;
        private readonly TelegramBotClient bot;
        private readonly App app;
        private readonly Command defaultCommand = new Command(RequestType.None, false);
        private readonly Dictionary<string, Command> commands;

        public TelegramBot(string token)
        {
            this.token = token;
            this.bot = new TelegramBotClient(token);
            this.app = new App();
            this.commands = GetCommands();
        }

        public Dictionary<string, Command> GetCommands()
        {
            return new Dictionary<string, Command>
            {
                {@"/exp", app.Commands[RequestType.Expression] },
                {@"/help", app.Commands[RequestType.Help] },
                {@"/roots", app.Commands[RequestType.Roots] },
                {@"/graphic", app.Commands[RequestType.Graphic] }
            };
        }

        public void Start()
        {
            bot.OnMessage += OnMessageReceived;
            bot.StartReceiving();
            while (true) ;
        }

        private async void OnMessageReceived(object sender, MessageEventArgs messageEvents)
        {
            var message = messageEvents.Message;

            if (message.Type == MessageType.Text)
            {
                var clientId = message.Chat.Id;
                var hasClient = app.TryGetClientById(clientId, out var client);

                if (!hasClient)
                {
                    client = new Client(clientId, defaultCommand);
                    app.AddClient(client);
                }
                var requestType = client.CurrentCommand.RequestType;
                var response = "";

                if (commands.ContainsKey(message.Text))
                {
                    var command = commands[message.Text];
                    if (!command.IsWaitingForResponse)
                    {
                        response = GetResponse(client, command.RequestType, "");
                        await bot.SendTextMessageAsync(message.Chat.Id, response);
                        app.ChangeClientCommand(client, defaultCommand);
                        return;
                    }

                    response = GetResponse(client, RequestType.WaitingForResult, ""); 
                    app.ChangeClientCommand(client, command);
                }
                else
                {
                    if (requestType != RequestType.None)
                        response = GetResponse(client, requestType, message.Text);
                    else
                        response = "Я не понимаю тебя...";
                }

                await bot.SendTextMessageAsync(message.Chat.Id, response);
            }
        }

        private string GetResponse(Client client, RequestType request, string message)
        {
            object result = null;

            switch (request)
            {
                case RequestType.Expression:
                    var data = ExpressionParser.Parse(message);
                    result = app.SolveClientTask(client, request, data);
                    return "Ответ: " + result.ToString();
                case RequestType.Roots:
                    var coeffs = PolynomParser.Parse(message);
                    var roots = (List<double>)app.SolveClientTask(client, request, coeffs);
                    return "Ответ: " + String.Join(", ", roots);
                case RequestType.Graphic:
                    return (string)app.SolveClientTask(client, request, message);
                case RequestType.Help:
                    return (string)app.SolveClientTask(client, request, null);
                default:
                    return (string)app.SolveClientTask(client, request, null);
            }
        }
    }
}
