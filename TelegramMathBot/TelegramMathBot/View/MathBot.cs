using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramMathBot.Domain;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View
{
    public class ReplyEventArgs : EventArgs
    {
        public IMessage Response { get; }
        public long ClientId { get; }
        public ReplyEventArgs(IMessage response, long clientId)
        {
            this.Response = response;
            this.ClientId = clientId;
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
        public void ProcessMessage(long clientId, string message)
        {
            var hasClient = clientManager.TryGetClientById(clientId, out var client);
            var unknownMessage = new TextMessage("I cant understand");
            IMessage response = null;

            if (!hasClient)
            {
                client = new Client(clientId);
                clientManager.AddClient(client);
            }

            if (commands.ContainsKey(message))
            {
                var command = commands[message];
                if (!command.IsWaitingClientInput)
                {
                    response = new TextMessage(command.GetHelpText());
                    OnReply?.Invoke(new ReplyEventArgs(response, clientId));
                    clientManager.ChangeClientCommand(client, null);
                    return;
                }

                response = new TextMessage(command.GetHelpText());
                clientManager.ChangeClientCommand(client, command);
            }
            else
            {
                if (client.CurrentCommand != null)
                    response = client.CurrentCommand.GetResponse(message);
                else
                    response = unknownMessage;
            }

            OnReply?.Invoke(new ReplyEventArgs(response, clientId));
        }
    }
}
