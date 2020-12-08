using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public class StartCommand : ICommand
    {
        public string HelpInfo => throw new NotImplementedException();

        public string Command => "/start";

        public ICommand GetNextCommand()
        {
            return null;
        }

        private string GetGreetingMessage()
        {
            var result = new StringBuilder();
            result.Append("Вас привествует математический бот, созданный специально для ");
            result.Append("курса ООП 2020.\n");
            result.Append("Введите команду /help, чтобы узнать больше");

            return result.ToString();
        }

        public IMessage GetResponse(string message)
        {
            return new TextMessage(GetGreetingMessage());
        }
    }
}
