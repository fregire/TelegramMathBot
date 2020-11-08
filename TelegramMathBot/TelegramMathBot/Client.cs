using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot
{
    public class Client
    {
        public long Id { get; }
        public Command Command { get; set; }
        public Client(long id, Command command = null)
        {
            Id = id;
            Command = command;
        }
    }
}
