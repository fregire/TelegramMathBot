using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ExpressionCommand
{
    public class ExpressionHelp : ICommand
    {
        public string HelpInfo => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";

        public string Command => "/exp";

        public ICommand GetNextCommand()
        {
            return new ExpresssionSolve();
        }

        public IMessage GetResponse(string message)
        {
            return new TextMessage("Введите численное выражение");
        }
    }
}
