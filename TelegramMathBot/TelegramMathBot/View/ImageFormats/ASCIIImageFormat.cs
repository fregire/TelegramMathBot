using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.ImageFormats
{
    public class ASCIIImageFormat : IImageFormat
    {
        public string Name => "ASCII";

        public IMessage GetResult(Image image)
        {
            return new TextMessage("Формат еще не готов");
        }
    }
}
