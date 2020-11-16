using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.Domain
{
    public class App
    {
        public Dictionary<long, Client> Clients { get; }
        public App()
        {
            this.Clients = new Dictionary<long, Client>();
        }

        public bool TryGetClientById(long id, out Client client)
        {
            client = null;

            if (Clients.ContainsKey(id))
            {
                client = Clients[id];
                return true;
            }

            return false;
        }

        public void AddClient(Client client)
        {
            Clients.Add(client.Id, client);
        }

        public void ChangeClientCommand(Client client, ICommand newCommand)
        {
            Clients[client.Id].CurrentCommand = newCommand;
        }
    }
}
