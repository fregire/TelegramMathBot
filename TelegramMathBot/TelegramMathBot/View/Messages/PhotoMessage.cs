using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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

        public PhotoMessage(System.Drawing.Bitmap image, ImageFormat imageFormat)
        {
            Image = GetImageFromBitmap(image, imageFormat);
            ImageFormat = imageFormat;
        }

        private Image GetImageFromBitmap(System.Drawing.Bitmap image, ImageFormat imageFormat)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, imageFormat);
                return new Image(stream);
            }
        }
    }
}
