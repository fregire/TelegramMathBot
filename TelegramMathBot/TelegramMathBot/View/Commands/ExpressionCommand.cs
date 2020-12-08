using System;
using System.Collections.Generic;
using System.Text;
using Domain.MathModule;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands
{
    public class ExpressionCommand : StepByStepCommand
    {
        public override string HelpInfo => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";

        public override string Command => "/exp";
        
        public override ICommand CreateSameCommand()
        {
            return new ExpressionCommand();
        }

        protected override List<Func<string, IMessage>> GetInitedCommands()
        {
            var result = new List<Func<string, IMessage>>();
            result.Add((input) =>
            {
                return new TextMessage("Введите численное выражение");
            });

            result.Add((input) =>
            {
                try
                {
                    var data = ExpressionParser.Parse(input);
                    var result = ExpressionSolver.Solve(data).ToString();
                    return new TextMessage(result);
                }
                catch
                {
                    return new TextMessage("Численное выражение введено неверно");
                }
            });

            return result;
        }

        protected override (bool IsCompleted, Func<string, IMessage> command) GetNextCommand()
        {
            var result = commands[currentCommand];
            currentCommand = currentCommand == commands.Count - 1
                ? currentCommand : currentCommand + 1;

            return (false, result);
        }
    }
}
