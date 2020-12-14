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
        public IntegralBounds(DefiniteIntegral definiteIntegral)
        {
            this.definiteIntegral = definiteIntegral;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var bounds = BoundsParser.Parse(message);
                definiteIntegral.LowerBound = bounds.LowerBound;
                definiteIntegral.UpperBound = bounds.UpperBound;

                var result = IntegralSolver.Solve(definiteIntegral);

                return (new IntegralFunction(), new TextMessage(result.ToString()));
            }
            catch
            {
                return (this, new TextMessage("Границы интегрирования заданы неверно"));
            }
        }
    }
}
