using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Commands
{
    public interface ICommand
    {
        public string GetResponse(string message);
        public string GetHelpText();
        public string Command { get; }
        public bool IsWaitingClientInput { get; }
    }
}
