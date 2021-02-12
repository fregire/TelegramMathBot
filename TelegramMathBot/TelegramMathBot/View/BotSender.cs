using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramMathBot.View.Messages;


namespace TelegramMathBot.View
{
    public class BotSender
    {
        private readonly TelegramBotClient bot;

        public BotSender(TelegramBotClient bot)
        {
            this.bot = bot;
        }

        public async void SendMessage(IMessage message, Chat chat)
        {
            if (message is TextMessage textMessage)
                await bot.SendTextMessageAsync(chat, textMessage.Text);

            if (message is PhotoMessage photoMessage)
            {
                var tmpName = GeneratePhotoName(photoMessage.ImageFormat);
                photoMessage.Image.SaveToFile(tmpName);

                using (var stream = System.IO.File.Open(tmpName, FileMode.Open))
                {
                    var fileToSend = new InputOnlineFile(stream, "Test");
                    await bot.SendPhotoAsync(chat, fileToSend);
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
