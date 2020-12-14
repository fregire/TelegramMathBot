﻿using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.IntegralCommand
{
    public class IntegralHelp : ICommand
    {
        public string HelpInfo => "Команда для вычисления определенного интеграла";

        public string Command => "/int";

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (new IntegralFunction(), new TextMessage("Введите функцию по переменной x"));
        }
    }
}