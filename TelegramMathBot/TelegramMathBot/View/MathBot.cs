using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramMathBot.Domain;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View
{
    public class MathBot
    {
        private readonly TelegramBot bot;
        private readonly App app;
        private readonly Dictionary<string, ICommand> commands;
        private readonly BotSender botSender;
        public MathBot(TelegramBot bot, App app, List<ICommand> commands)
        {
            // MathBot и TelegramBot независимы (через события привязать)
            this.bot = bot;
            this.app = app;
            this.commands = GetDictCommands(commands);
            this.botSender = new BotSender(bot);

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
            // Возвращать объекты, а не строки
            // Промежуточный класс - обертка для Telegram и Math ботов который 
            // принимает объект, возвращенный из MessageTextReceived - результат и переводит в команды для бота
            var clientId = messageTextEventArgs.Id;
            var message = messageTextEventArgs.Message;
            var hasClient = app.TryGetClientById(clientId, out var client);
            IMessage response = null;

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
                    response = new TextMessage(command.GetHelpText());
                    botSender.SendMessage(response, clientId);
                    app.ChangeClientCommand(client, null);
                    return;
                }

                response = new TextMessage(command.GetHelpText());
                app.ChangeClientCommand(client, command);
            }
            else
            {
                if (client.CurrentCommand != null)
                    response = client.CurrentCommand.GetResponse(message);
                else
                    response = new TextMessage("I cant understand");
            }

            botSender.SendMessage(response, clientId);
        }
    }
}
