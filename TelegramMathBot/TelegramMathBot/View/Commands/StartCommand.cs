using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public class StartCommand : ICommand
    {
        public string FullDescription => throw new NotImplementedException();

        public string Command => "/start";

        public string Description => "Начало работы с ботом";

        private string GetGreetingMessage()
        {
            var result = new StringBuilder();
            result.Append("Вас привествует математический бот, созданный специально для ");
            result.Append("курса ООП 2020.\n");
            result.Append("Введите команду /help, чтобы узнать больше");

            return result.ToString();
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (new UnknownCommand(), new TextMessage(GetGreetingMessage()));
        }
    }
}
