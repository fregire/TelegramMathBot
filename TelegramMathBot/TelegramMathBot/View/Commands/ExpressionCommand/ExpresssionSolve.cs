using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.ExpressionCommand
{
    public class ExpresssionSolve : ICommand
    {
        public string HelpInfo => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        public ICommand GetNextCommand()
        {
            return this;
        }

        public IMessage GetResponse(string message)
        {
            try
            {
                var data = ExpressionParser.Parse(message);
                var result = ExpressionSolver.Solve(data).ToString();
                return new TextMessage(result);
            }
            catch
            {
                return new TextMessage("Численное выражение введено неверно");
            }
        }
    }
}
