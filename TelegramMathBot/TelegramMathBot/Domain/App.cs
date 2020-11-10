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
            Commands.Add(RequestType.Graphic, new Command(RequestType.Graphic, false));
            Commands.Add(RequestType.Roots, new Command(RequestType.Roots, true));
        }


        public object SolveClientTask(Client client, RequestType requestType, params object[] args)
        {
            //object -> solverresult тип 
            // Тексты во view перенести
            // передавать solver'ы через конструкторы
            // Разбить app на части 
            object result;

            switch (requestType)
            {
                case RequestType.WaitingForResult:
                    result = "Напишите выражение";
                    break;
                case RequestType.Expression:
                    result = ExpressionSolver.Solve((string)args[0]);
                    break;
                case RequestType.Graphic:
                    result = "К сожалению, пока в доработке";
                    break;
                case RequestType.Roots:
                    result = PolynomialSolver.Solve((List<double>)args[0]);
                    break;
                case RequestType.Help:
                    result = 
                        "Команды: /help, exp, roots, graphic\n\n" +
                        "/help - помощь по боту\n\n" +
                        "/exp - решение числовых выражений - отправьте команду 'exp'" +
                        " а затем отправьте выражение, которое необходимо посчитать\n\n" +
                        "/roots - поиск корней уравнение - отправьте команду 'roots'," +
                        "а затем отправьте уравнение в формате ax^n + bx^(n-1) + ... + c\n" +
                        "Например: 2x^2 + x + 9";
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
