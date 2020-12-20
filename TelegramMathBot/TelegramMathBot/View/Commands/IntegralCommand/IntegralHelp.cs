using Domain.AdditionalMath;
using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.IntegralCommand
{
    public class IntegralHelp : ICommand
    {
        public string FullDescription => "Команда для вычисления определенного интеграла";
        public string Description => "Вычисление определенного интеграла";
        public string Command => "/int";

        private readonly ISolver<DefiniteIntegral, double> integralSolver;
        private readonly IParser<string, Func<double, double>> funcParser;
        private readonly IParser<string, (double LowerBound, double UpperBound)> boundsParser;
        public IntegralHelp(
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
            return (
                new IntegralFunction(integralSolver, funcParser, boundsParser), 
                new TextMessage("Введите функцию по переменной x"));
        }
    }
}
