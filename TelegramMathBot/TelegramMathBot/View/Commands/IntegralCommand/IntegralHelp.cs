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

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (
                new IntegralFunction(new FunctionParser()), 
                new TextMessage("Введите функцию по переменной x"));
        }
    }
}
