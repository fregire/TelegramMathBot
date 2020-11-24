using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using TelegramMathBot.Domain;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View
{
    public class ReplyEventArgs : EventArgs
    {
        public IMessage Response { get; }
        public Chat ClientChat { get; }
        public ReplyEventArgs(IMessage response, Chat chat)
        {
            this.Response = response;
            this.ClientChat = chat;
        }
    }


    public delegate void ReplyHandler(ReplyEventArgs replyEventArgs);

    public class MathBot
    {
        private readonly ClientManager clientManager;
        private readonly Dictionary<string, ICommand> commands;
        public MathBot(ClientManager app, List<ICommand> commands)
        {
            this.clientManager = app;
            this.commands = GetDictCommands(commands);
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

        public event ReplyHandler OnReply;
        public void ProcessMessage(Message message)
        {
            var clientChatId = message.Chat.Id;
            var text = message.Text;
            var hasClient = clientManager.TryGetClientById(clientChatId, out var client);
            var unknownMessage = new TextMessage("I cant understand");
            IMessage response = null;

            if (!hasClient)
            {
                client = new Client(clientChatId);
                clientManager.AddClient(client);
            }

            if (commands.ContainsKey(text))
            {
                var command = commands[text];
                if (!command.IsWaitingClientInput)
                {
                    response = new TextMessage(command.GetHelpText());
                    OnReply?.Invoke(new ReplyEventArgs(response, message.Chat));
                    clientManager.ChangeClientCommand(client, null);
                    return;
                }

                response = new TextMessage(command.GetHelpText());
                clientManager.ChangeClientCommand(client, command);
            }
            else
            {
                if (client.CurrentCommand != null)
                    response = client.CurrentCommand.GetResponse(text);
                else
                    response = unknownMessage;
            }

            OnReply?.Invoke(new ReplyEventArgs(response, message.Chat));
        }
    }
}
