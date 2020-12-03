using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace TelegramMathBot.View.Messages
{
    public class PhotoMessage : IMessage
    {
        public MessageType MessageType => MessageType.Photo;
        public Image Image { get; }
        public ImageFormat ImageFormat { get; }
        public PhotoMessage(Image image, ImageFormat imageFormat)
        {
            Image = image;
            ImageFormat = imageFormat;
        }
    }
}
