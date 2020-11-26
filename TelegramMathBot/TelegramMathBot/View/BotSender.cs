using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View
{
    public class BotSender
    {
        private readonly TelegramBot bot;

        public BotSender(TelegramBot bot)
        {
            this.bot = bot;
        }

        public void SendMessage(IMessage message, Chat chat)
        {
            if (message is TextMessage textMessage)
            {
                var content = new Message { Chat = chat, Text = textMessage.Text };
                bot.SendTextMessage(content);
            }
                
        }
    }
}
