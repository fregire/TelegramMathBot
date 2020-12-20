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
        private readonly ISolver<DefiniteIntegral, double> integralSolver;
        private readonly IParser<string, Func<double, double>> funcParser;
        private readonly IParser<string, (double LowerBound, double UpperBound)> boundsParser;
        public IntegralBounds(
            ISolver<DefiniteIntegral, double> integralSolver,
            IParser<string, Func<double, double>> funcParser,
            IParser<string, (double LowerBound, double UpperBound)> boundsParser,
            DefiniteIntegral definiteIntegral)
        {
            this.integralSolver = integralSolver;
            this.boundsParser = boundsParser;
            this.definiteIntegral = definiteIntegral;
            this.funcParser = funcParser;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var bounds = boundsParser.Parse(message);
                definiteIntegral.LowerBound = bounds.LowerBound;
                definiteIntegral.UpperBound = bounds.UpperBound;

                var result = integralSolver.Solve(definiteIntegral);

                return (
                    new IntegralFunction(integralSolver, funcParser, boundsParser), 
                    new TextMessage(result.ToString()));
            }
            catch
            {
                return (this, new TextMessage("Границы интегрирования заданы неверно"));
            }
        }
    }
}
