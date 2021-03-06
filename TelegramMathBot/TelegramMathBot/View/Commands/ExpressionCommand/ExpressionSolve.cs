﻿using Domain.MathModule;
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

        private readonly ISolver<string, decimal> expSolver;
        private readonly IParser<string, string> expParser;
        public ExpressionSolve(
            ISolver<string, decimal> expSolver, 
            IParser<string, string> expParser)
        {
            this.expSolver = expSolver;
            this.expParser = expParser;
        }
        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var data = expParser.Parse(message);
                var result = expSolver.Solve(data).ToString();
                return (this, new TextMessage(result));
            }
            catch
            {
                return (this, new TextMessage("Численное выражение введено неверно"));
            }
        }
    }
}
