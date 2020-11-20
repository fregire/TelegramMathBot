using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Messages
{
    public class TextMessage : IMessage
    {
        public string Text { get; }
        public MessageType MessageType => MessageType.Text;
        public TextMessage(string text)
        {
            Text = text;
        }
    }
}
