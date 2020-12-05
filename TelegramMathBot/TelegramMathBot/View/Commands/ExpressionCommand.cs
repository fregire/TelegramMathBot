using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands
{
    public class ExpressionCommand : ICommand
    {
        public string Command => "/exp";

        public string HelpInfo => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";

        private int currentCommand;
        private readonly List<Func<string, IMessage>> commands;
        public (bool IsCompleted, IMessage Response) GetResponse(string input)
        {
            var result = commands[currentCommand].Invoke(input);
            currentCommand = currentCommand == commands.Count - 1
                ? currentCommand : currentCommand + 1;

            return (false, result);
        }

        public ICommand CreateSameCommand()
        {
            return new ExpressionCommand();
        }

        public ExpressionCommand()
        {
            commands = new List<Func<string, IMessage>>();
            InitCommands(commands);
        }

        private void InitCommands(List<Func<string, IMessage>> commands)
        {
            commands.Add((input) =>
            {
                return new TextMessage("Введите численное выражение");
            });

            commands.Add((input) =>
            {
                var data = ExpressionParser.Parse(input);
                var result = ExpressionSolver.Solve(data).ToString();

                return new TextMessage(result);
            });
        }
    }
}
