using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public class UnknownCommand : ICommand
    {
        public string HelpInfo => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (this, new TextMessage("Я не понимаю:( \nВведите команду /help"));
        }
    }
}
