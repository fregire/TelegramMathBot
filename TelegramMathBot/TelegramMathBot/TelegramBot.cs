using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramMathBot.Parsers;

namespace TelegramMathBot
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
                {"exp", new Command(RequestType.Expression, true) },
                {"fac", new Command(RequestType.Factorial, true) },
                {"/help", new Command(RequestType.Help, false) }
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
                var requestType = client.State.RequestType;
                string response;

                if (requestType == RequestType.None)
                {
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

                        app.ChangeClientCommand(client, command);
                        response = GetResponse(client, RequestType.None, "");
                    }
                    else
                        response = "Can't understand";
                }
                else
                    response = GetResponse(client, requestType, message.Text);

                await bot.SendTextMessageAsync(message.Chat.Id, response);
            }
        }

        private string GetResponse(Client client, RequestType request, string message)
        {
            switch (request)
            {
                case RequestType.Expression:
                    var data = ExpressionParser.Parse(message);
                    return app.SolveClientTask(client, request, data);
                default:
                    return "Type expression";
            }
        }
    }
}
