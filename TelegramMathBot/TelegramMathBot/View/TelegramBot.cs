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
using Telegram.Bot.Types;

namespace TelegramMathBot.View
{
    public class MessageTextEventArgs: EventArgs
    {
        public Message Message { get; }

        public MessageTextEventArgs(Message message)
        {
            Message = message;
        }
    }

    public delegate void MessageTextReceivedHandler(MessageTextEventArgs messageEventArgs);

    public class TelegramBot
    {
        private readonly TelegramBotClient bot;
        public event MessageTextReceivedHandler OnMessageTextReceived;

        public TelegramBot(TelegramBotClient bot)
        {
            this.bot = bot;
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
                OnMessageTextReceived?.Invoke(new MessageTextEventArgs(message));
        }

        public async void SendTextMessage(Message message)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, message.Text);
        }

        public async void SendPhotoMessage(Chat chat, InputOnlineFile file)
        {
            await bot.SendPhotoAsync(chat.Id, file);
        }
    }
}
