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

        public string HelpInfo => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";

        public string UserInputTip => "Введите численное выражение";

        public IMessage GetResponse(string input)
        {
            var data = ExpressionParser.Parse(input);
            var result = ExpressionSolver.Solve(data).ToString();

            return new TextMessage(result);
        }
    }
}
