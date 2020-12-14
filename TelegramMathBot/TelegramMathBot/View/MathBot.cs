using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
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
        private readonly Dictionary<Client, ICommand> clientsCommands;
        public Dictionary<string, ICommand> Commands { get; }
        public MathBot(ClientManager app, List<ICommand> commands)
        {
            this.clientManager = app;
            this.Commands = GetDictCommands(commands);
            this.clientsCommands = new Dictionary<Client, ICommand>();
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
            var defaultCommand = new UnknownCommand();

            if (!hasClient)
            {
                client = new Client(clientChatId);
                clientManager.AddClient(client);
                clientsCommands.Add(client, defaultCommand);
            }

            var currCommand = clientsCommands[client];

            if (Commands.ContainsKey(text))
                currCommand = Commands[text];

            var commandResult = currCommand.GetResponse(text);
            var response = commandResult.Response;
            clientsCommands[client] = commandResult.NextCommand;

            OnReply?.Invoke(new ReplyEventArgs(response, message.Chat));
        }
    }
}
