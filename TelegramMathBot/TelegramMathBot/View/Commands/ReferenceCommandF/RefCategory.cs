using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ReferenceCommandF
{
    public class RefCategory
    {
        public string Name { get; }
        public IMessage Message { get; }
        public RefCategory(string name, IMessage message)
        {
            Name = name;
            Message = message;
        }
    }
}
