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
        private readonly ISolver<DefiniteIntegral, double> solver;
        public IntegralBounds(
            ISolver<DefiniteIntegral, double> solver, 
            DefiniteIntegral definiteIntegral)
        {
            this.definiteIntegral = definiteIntegral;
            this.solver = solver;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var bounds = BoundsParser.Parse(message);
                definiteIntegral.LowerBound = bounds.LowerBound;
                definiteIntegral.UpperBound = bounds.UpperBound;

                var result = solver.Solve(definiteIntegral);

                return (new IntegralFunction(), new TextMessage(result.ToString()));
            }
            catch
            {
                return (this, new TextMessage("Границы интегрирования заданы неверно"));
            }
        }
    }
}
