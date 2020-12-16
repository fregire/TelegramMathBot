using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.ImageFormats
{
    public class PNGImageFormat : IImageFormat
    {
        public string Name => "PNG";

        public IMessage GetResult(Image image)
        {
            return new PhotoMessage(image, ImageFormat.Png);
        }
    }
}
