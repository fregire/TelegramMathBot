using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Messages
{
    public interface IMessage
    {
        public MsgType MsgType { get; }
    }
}
