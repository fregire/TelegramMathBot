using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.ExpressionCommand
{
    public class ExpressionSolve : ICommand
    {
        public string FullDescription => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        //Явно зависит от ExpressionParser => зависимость
        private readonly ISolver<string, decimal> expressionSolver;
        public ExpressionSolve(ISolver<string, decimal> solver)
        {
            this.expressionSolver = solver;
        }
        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var data = ExpressionParser.Parse(message);
                var result = expressionSolver.Solve(data).ToString();
                return (this, new TextMessage(result));
            }
            catch
            {
                return (this, new TextMessage("Численное выражение введено неверно"));
            }
        }
    }
}
