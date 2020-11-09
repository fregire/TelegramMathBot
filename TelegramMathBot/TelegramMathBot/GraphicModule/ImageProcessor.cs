using System;
using SFML.Graphics;

namespace TelegramMathBot.GraphicModule
{
    public class ImageProcessor
    {
        public Image GraphImage;
        public string Filename;
        public Tuple<double, double> XInterval;
        public Tuple<double, double> YInterval;
        public uint Width;
        public uint Height;

        public ImageProcessor(uint width, uint height, string filename, Tuple<double, double> xInterval,
            Tuple<double, double> yInterval)
        {
            Width = width;
            Height = height;
            GraphImage = new Image(width, height, Color.White);
            XInterval = xInterval;
            YInterval = yInterval;
            Filename = filename;
        }

        public void DrawAxes()
        {
            var niceGrey = new Color(150, 150, 150);
            for (var x = Math.Ceiling(XInterval.Item1); x <= Math.Floor(XInterval.Item2); x += 1)
            {
                var screenPoint = TransofmPosition(new Tuple<double, double>(x, 0));
                if (screenPoint == null) continue;
                var imageX = screenPoint.Item1;
                for (uint imageY = 0; imageY <= Height; imageY++)
                {
                    GraphImage.SetPixel(imageX, imageY, niceGrey);
                }
            }

            for (var y = Math.Ceiling(YInterval.Item1); y <= Math.Floor(YInterval.Item2); y += 1)
            {
                var screenPoint = TransofmPosition(new Tuple<double, double>(0, y));
                if (screenPoint == null) continue;
                var imageY = screenPoint.Item2;
                for (uint imageX = 0; imageX <= Width; imageX++)
                {
                    GraphImage.SetPixel(imageX, imageY, niceGrey);
                }
            }

            var zeroPoint = TransofmPosition(new Tuple<double, double>(0, 0));
            for (uint x = 0; x < Width; x++) GraphImage.SetPixel(x, zeroPoint.Item2, Color.Black);
            for (uint y = 0; y < Height; y++) GraphImage.SetPixel(zeroPoint.Item1, y, Color.Black);
        }

        public void DrawGraphic()
        {
            
        }

        public Tuple<uint, uint> TransofmPosition(Tuple<double, double> point)
        {
            var pointX = (uint) ((point.Item1 - XInterval.Item1) / (XInterval.Item2 - XInterval.Item1) * Width);
            var pointY = (uint) ((point.Item2 - YInterval.Item1) / (YInterval.Item2 - YInterval.Item1) * Height);
            if (pointX < 0 || pointX > Width || pointY < 0 || pointY > Height) return null;
            return new Tuple<uint, uint>(pointX, pointY);
        }

        public bool SaveFile() => GraphImage.SaveToFile(Filename);
    }
}