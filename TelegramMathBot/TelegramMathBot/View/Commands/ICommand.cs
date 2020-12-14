using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public interface ICommand
    {
        public (ICommand NextCommand, IMessage Response) GetResponse(string message);
        public string FullDescription { get; }
        public string Description { get; }
        public string Command { get; }
    }
}
