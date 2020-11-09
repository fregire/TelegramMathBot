using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TelegramMathBot.Math;

namespace TelegramMathBot
{
    public class App
    {
        public Dictionary<long, Client> Clients { get; }
        public readonly Command DefaultCommand = new Command(RequestType.None, false);
        public App()
        {
            this.Clients = new Dictionary<long, Client>();
        }

        public string SolveClientTask(Client client, RequestType requestType, params object[] args)
        {
            var result = "";

            switch (requestType)
            {
                case RequestType.Expression:
                    result = ExpressionSolver.Solve((string)args[0]).ToString();
                    break;
                default:
                    result = "None";
                    break;
            }

            ChangeClientCommand(client, DefaultCommand);

            return result;
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

        public void ChangeClientCommand(Client client, Command newCommand)
        {
            Clients[client.Id].State = newCommand;
        }
    }
}
