using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.Domain
{
    public class App
    {
        public Dictionary<long, Client> Clients { get; }
        public readonly Command DefaultCommand;
        public Dictionary<RequestType, Command> Commands { get; }
        public App()
        {
            this.Clients = new Dictionary<long, Client>();
            this.Commands = new Dictionary<RequestType, Command>();
            this.DefaultCommand = new Command(RequestType.None, false);

            InitCommands();
        }

        private void InitCommands()
        {
            Commands.Add(RequestType.Expression, new Command(RequestType.Expression, true));
            Commands.Add(RequestType.Help, new Command(RequestType.Help, false));
            Commands.Add(RequestType.Graphic, new Command(RequestType.Graphic, true));
        }


        public object SolveClientTask(Client client, RequestType requestType, params object[] args)
        {
            object result;

            switch (requestType)
            {
                case RequestType.Expression:
                    result = ExpressionSolver.Solve((string)args[0]);
                    break;
                case RequestType.Graphic:
                    GraphicSolver.Solve(500,
                                500,
                                client.Id + "graphic.png",
                                Tuple.Create(-1.0, 1.0),
                                Tuple.Create(-1.0, 1.0),
                                (Func<double, double>)args[0]);
                    result = "Graphic";
                    break;
                case RequestType.Help:
                    result = "Hello, World!";
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
            Clients[client.Id].CurrentCommand = newCommand;
        }
    }
}
