using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramMathBot.Domain;
using TelegramMathBot.View.Commands;

namespace TelegramMathBot.View
{
    public class MathBot
    {
        private readonly TelegramBot bot;
        private readonly App app;
        private readonly Dictionary<string, ICommand> commands;
        public MathBot(TelegramBot bot, App app, List<ICommand> commands)
        {
            this.bot = bot;
            this.app = app;
            this.commands = GetDictCommands(commands);

            this.bot.OnMessageTextReceived += MessageTextReceived;
        }

        private Dictionary<string, ICommand> GetDictCommands(List<ICommand> commands)
        {
            return commands
                .Aggregate(
                    new Dictionary<string, ICommand>(),
                    (res, elem) =>
                    {
                        res.Add(elem.Command, elem);
                        return res;
                 });
        }

        public void Start()
        {
            bot.StartReceiving();
        }

        private void MessageTextReceived(MessageTextEventArgs messageTextEventArgs)
        {
            var clientId = messageTextEventArgs.Id;
            var message = messageTextEventArgs.Message;
            var hasClient = app.TryGetClientById(clientId, out var client);
            string response;

            if (!hasClient)
            {
                client = new Client(clientId);
                app.AddClient(client);
            }

            if (commands.ContainsKey(message))
            {
                var command = commands[message];
                if (!command.IsWaitingClientInput)
                {
                    response = command.GetHelpText();
                    bot.SendTextMessage(clientId, response);
                    app.ChangeClientCommand(client, null);
                    return;
                }

                response = command.GetHelpText();
                app.ChangeClientCommand(client, command);
            }
            else
            {
                if (client.CurrentCommand != null)
                    response = client.CurrentCommand.GetResponse(message);
                else
                    response = "Я не понимаю тебя...";
            }

            bot.SendTextMessage(clientId, response);
        }
    }
}
