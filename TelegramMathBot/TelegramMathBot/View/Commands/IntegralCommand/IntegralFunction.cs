using Domain.AdditionalMath;
using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.IntegralCommand
{
    public class IntegralFunction : ICommand
    {
        public string FullDescription => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();

        private readonly ISolver<DefiniteIntegral, double> integralSolver;
        private readonly IParser<string, Func<double, double>> funcParser;
        private readonly IParser<string, (double LowerBound, double UpperBound)> boundsParser;
        public IntegralFunction(
            ISolver<DefiniteIntegral, double> integralSolver,
            IParser<string, Func<double, double>> funcParser,
            IParser<string, (double LowerBound, double UpperBound)> boundsParser)
        {
            this.integralSolver = integralSolver;
            this.funcParser = funcParser;
            this.boundsParser = boundsParser;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var func = funcParser.Parse(message);
                var integral = new DefiniteIntegral { Function = func };

                return (
                    new IntegralBounds(
                        integralSolver, 
                        funcParser,
                        boundsParser,
                        integral), 
                    new TextMessage("Введите границы интегрирования в формате: a,b (a-нижняя граница, b - верхняя) \n" +
                    "Например, 2,4 (от 2 до 4)"));
            }
            catch
            {
                return (this, new TextMessage("Функция неверна"));
            }
        }
    }
}
