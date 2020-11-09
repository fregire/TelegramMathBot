using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.Domain
{
    public class Client
    {
        public long Id { get; }
        public Command CurrentCommand { get; set; }
        public Client(long id, Command command = null)
        {
            Id = id;
            CurrentCommand = command;
        }
    }
}
