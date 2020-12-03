using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
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

            if (message is PhotoMessage photoMessage)
            {
                var tmpName = GeneratePhotoName(photoMessage.ImageFormat);
                photoMessage.Image.SaveToFile(tmpName);

                using (var stream = System.IO.File.Open(tmpName, FileMode.Open))
                {
                    var fileToSend = new InputOnlineFile(stream, "Test");
                    bot.SendPhotoMessage(chat, fileToSend);
                    Thread.Sleep(1);
                }

                System.IO.File.Delete(tmpName);
            }
        }

        private string GeneratePhotoName(ImageFormat format)
        {
            var name = new object().GetHashCode();

            return name + "." + format.ToString().ToLower();
        }
    }
}
