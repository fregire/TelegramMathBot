using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramMathBot
{
    public class MathBot
    {
        private readonly string token;
        private readonly TelegramBotClient bot;
        public MathBot(string token)
        {
            this.token = token;
            this.bot = new TelegramBotClient(token);
            this.bot.OnMessage += OnMessageReceived;
        }

        public void Start()
        {   
            bot.StartReceiving();
            while (true);
        }

        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Console.WriteLine(messageEventArgs.Message.Text);
        }
    }
}
