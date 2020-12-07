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
        public Client(long id)
        {
            ClientId = new ClientId(id);
        }
    }
}
