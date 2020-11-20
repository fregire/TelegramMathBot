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

        public bool IsWaitingClientInput => true;

        public string GetHelpText()
        {
            return "Вычисление выражения";
        }

        public IMessage GetResponse(string input)
        {
            var data = ExpressionParser.Parse(input);
            var result = ExpressionSolver.Solve(data).ToString();

            return new TextMessage(result);
        }
    }
}
