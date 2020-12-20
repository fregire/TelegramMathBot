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

        private readonly ExpressionSolver solver;
        private readonly ExpressionParser parser;
        public ExpressionSolve(ExpressionSolver solver, ExpressionParser parser)
        {
            this.solver = solver;
            this.parser = parser;
        }
        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var data = parser.Parse(message);
                var result = solver.Solve(data).ToString();
                return (this, new TextMessage(result));
            }
            catch
            {
                return (this, new TextMessage("Численное выражение введено неверно"));
            }
        }
    }
}
