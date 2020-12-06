﻿using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public interface ICommand
    {
        public (bool IsCompleted, IMessage Response) GetResponse(string message);
        public string HelpInfo { get; }
        public string Command { get; }
        public ICommand CreateSameCommand();
    }
}