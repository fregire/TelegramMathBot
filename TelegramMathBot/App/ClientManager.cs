using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class ClientManager
    {
        private readonly Dictionary<long, Client> clients;
        public ClientManager()
        {
            clients = new Dictionary<long, Client>();
        }

        public bool TryGetClientById(long id, out Client client)
        {
            client = null;

            if (clients.ContainsKey(id))
            {
                client = clients[id];
                return true;
            }

            return false;
        }

        public void AddClient(Client client)
        {
            clients.Add(client.ClientId.Id, client);
        }
    }
}
