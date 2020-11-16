using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramMathBot.View.Parsers;
using TelegramMathBot.Domain;
using Telegram.Bot.Types.InputFiles;
using System.IO;
using System.Linq;

namespace TelegramMathBot.View
{
    public class MessageTextEventArgs: EventArgs
    {
        public string Message { get; }
        public long Id { get; }


        public MessageTextEventArgs(string message, long messageId)
        {
            Message = message;
            Id = messageId;
        }
    }

    public delegate void MessageTextReceivedHandler(MessageTextEventArgs messageEventArgs);

    public class TelegramBot
    {
        private readonly TelegramBotClient bot;
        public event MessageTextReceivedHandler OnMessageTextReceived;

        public TelegramBot(string token)
        {
            this.bot = new TelegramBotClient(token);
        }

        public void StartReceiving()
        {
            bot.OnMessage += OnMessageReceived;
            bot.StartReceiving();
        }

        private void OnMessageReceived(object sender, MessageEventArgs messageEvents)
        {
            var message = messageEvents.Message;

            if (message.Type == MessageType.Text)
                OnMessageTextReceived?.Invoke(new MessageTextEventArgs(message.Text, message.Chat.Id));
        }

        public async void SendTextMessage(long chatId, string message)
        {
            await bot.SendTextMessageAsync(chatId, message);
        }
    }
}
