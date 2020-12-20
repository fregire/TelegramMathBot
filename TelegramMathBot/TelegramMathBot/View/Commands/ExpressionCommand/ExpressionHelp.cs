using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ExpressionCommand
{
    public class ExpressionHelp : ICommand
    {
        public string FullDescription => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";
        public string Description => "Вычисление выражений";
        public string Command => "/exp";

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (
                new ExpressionSolve(new ExpressionSolver()), 
                new TextMessage("Введите численное выражение"));
        }
    }
}
