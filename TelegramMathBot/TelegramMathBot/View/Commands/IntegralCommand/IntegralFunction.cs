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
        public string HelpInfo => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var func = FunctionParser.Parse(message);
                var integral = new DefiniteIntegral { Function = func };

                return (
                    new IntegralBounds(integral), 
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
