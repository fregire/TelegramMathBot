using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramMathBot
{
    public class TelegramBot
    {
        private readonly string token;
        private readonly TelegramBotClient bot;
        private readonly App app;
        private readonly Command defaultCommand = new Command(RequestType.None, false);

        public TelegramBot(string token)
        {
            this.token = token;
            this.bot = new TelegramBotClient(token);
            this.app = new App(GetCommands());
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
                var requestType = client.Command.RequestType;
                string response;

                if (requestType == RequestType.None)
                {
                    if (app.Commands.ContainsKey(message.Text))
                    {
                        var command = app.Commands[message.Text];
                        if (!command.IsWaitingForResponse)
                        {
                            response = GetResponse(command.RequestType, "");
                            await bot.SendTextMessageAsync(message.Chat.Id, response);
                            client.Command = defaultCommand;
                            return;
                        }

                        client.Command = command;
                        response = GetResponse(RequestType.None, "");
                    }
                    else
                        response = "Can't understand";
                }
                else
                {
                    response = GetResponse(requestType, message.Text);
                    client.Command = defaultCommand;
                }

                await bot.SendTextMessageAsync(message.Chat.Id, response);
            }
        }

        private string GetResponse(RequestType request, string message)
        {
            switch (request)
            {
                case RequestType.Expression:
                    return "Done";
                case RequestType.Factorial:
                    return "Done";
                case RequestType.Help:
                    return "Helping instructions";
                case RequestType.None:
                    return "Type expression";
                default:
                    return "Nigger";
            }
        }
    }
}
