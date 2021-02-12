using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Messages
{
    public class TextMessage : IMessage
    {
        public string Text { get; }
        public MsgType MsgType => MsgType.Text;
        public TextMessage(string text)
        {
            Text = text;
        }
    }
}
