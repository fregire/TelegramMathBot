using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Commands;

namespace TelegramMathBot.Domain
{
    public class Client
    {
        public long Id { get; }
        public ICommand CurrentCommand { get; set; }
        public Client(long id, ICommand command = null)
        {
            Id = id;
            CurrentCommand = command;
        }
    }
}
