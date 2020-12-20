using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.ExpressionCommand
{
    public class ExpressionHelp : ICommand
    {
        public string FullDescription => "Команда для вычисления численного выражения.\n" +
            "Например, 6+2-10*4+6!";
        public string Description => "Вычисление выражений";
        public string Command => "/exp";
        private readonly ISolver<string, decimal> expSolver;
        private readonly IParser<string, string> expParser;
        public ExpressionHelp(
            ISolver<string, decimal> expSolver, 
            IParser<string, string> expParser)
        {
            this.expParser = expParser;
            this.expSolver = expSolver;
        }
        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (
                new ExpressionSolve(expSolver,  expParser), 
                new TextMessage("Введите численное выражение"));
        }
    }
}
