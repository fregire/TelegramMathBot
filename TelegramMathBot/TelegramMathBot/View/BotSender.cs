using System;
using System.Collections.Generic;
using System.Text;
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

        public void SendMessage(IMessage message, long chatId)
        {
            if (message is TextMessage textMessage)
                bot.SendTextMessage(chatId, textMessage.Text);
        }
    }
}
