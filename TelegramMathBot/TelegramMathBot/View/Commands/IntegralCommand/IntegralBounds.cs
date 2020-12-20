using Domain.AdditionalMath;
using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.IntegralCommand
{
    public class IntegralBounds : ICommand
    {
        public string FullDescription => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();
        private readonly DefiniteIntegral definiteIntegral;
        private readonly IntegralSolver solver;
        private readonly BoundsParser parser;
        public IntegralBounds(
            IntegralSolver solver, 
            BoundsParser parser,
            DefiniteIntegral definiteIntegral)
        {
            this.definiteIntegral = definiteIntegral;
            this.solver = solver;
            this.parser = parser;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var bounds = parser.Parse(message);
                definiteIntegral.LowerBound = bounds.LowerBound;
                definiteIntegral.UpperBound = bounds.UpperBound;

                var result = solver.Solve(definiteIntegral);

                return (
                    new IntegralFunction(new FunctionParser()), 
                    new TextMessage(result.ToString()));
            }
            catch
            {
                return (this, new TextMessage("Границы интегрирования заданы неверно"));
            }
        }
    }
}
