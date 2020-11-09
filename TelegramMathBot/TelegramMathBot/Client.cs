using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot
{
    public class Client
    {
        public long Id { get; }
        public Command State { get; set; }
        public Client(long id, Command command = null)
        {
            Id = id;
            State = command;
        }
    }
}
