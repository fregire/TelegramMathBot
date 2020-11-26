using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using TelegramMathBot.View.Commands;

namespace TelegramMathBot.Domain
{
    public class Client
    {
        public ClientId ClientId { get; }
        public ICommand CurrentCommand { get; set; }
        public Client(long id, ICommand command = null)
        {
            ClientId = new ClientId(id);
            CurrentCommand = command;
        }
    }
}
