using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegramMathBot
{
    public class App
    {
        public Dictionary<string, Command> Commands { get; }
        public Dictionary<long, Client> Clients { get; }
        public App(Dictionary<string, Command> commands)
        {
            this.Commands = commands;
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
    }
}
