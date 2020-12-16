using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.ImageFormats
{
    public interface IImageFormat
    {
        public string Name { get; }
        public IMessage GetResult(Image image);
    }
}
