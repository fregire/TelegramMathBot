using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Domain.GraphicModule
{
    public class ImageProcessor
    {
        public Image GraphImage;
        public string Filename;
        public Tuple<double, double> XInterval;
        public Tuple<double, double> YInterval;
        public uint Width;
        public uint Height;

        public ImageProcessor(uint width, uint height, Tuple<double, double> xInterval,
            Tuple<double, double> yInterval)
        {
            Width = width;
            Height = height;
            GraphImage = new Image(width, height, Color.White);
            XInterval = xInterval;
            YInterval = yInterval;
        }

        public void DrawAxes()
        {
            var niceGrey = new Color(150, 150, 150);
            /*
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
            */
            var zeroPoint = TransofmPosition(new Tuple<double, double>(0, 0));
            for (uint x = 0; x < Width; x++) GraphImage.SetPixel(x, zeroPoint.Item2, Color.Black);
            for (uint y = 0; y < Height; y++) GraphImage.SetPixel(zeroPoint.Item1, y, Color.Black);
        }
        
        public void DrawGraphic(List<Tuple<double, double>> points, Color color)
        {
            foreach (var point in points)
            {
                var p = TransofmPosition(point);
                if (p.Item1 <= 1 || p.Item1 >= Width - 1 || p.Item2 <= 1 || p.Item2 >= Height - 1) continue;
                GraphImage.SetPixel(p.Item1, p.Item2, color);
                GraphImage.SetPixel(p.Item1 + 1, p.Item2, color);
                GraphImage.SetPixel(p.Item1 - 1, p.Item2, color);
                GraphImage.SetPixel(p.Item1, p.Item2 + 1, color);
                GraphImage.SetPixel(p.Item1, p.Item2 - 1, color);
            }
        }

        public Tuple<uint, uint> TransofmPosition(Tuple<double, double> point)
        {
            var pointX = (uint)((point.Item1 - XInterval.Item1) / (XInterval.Item2 - XInterval.Item1) * Width);
            var pointY = (uint)(Height - (point.Item2 - YInterval.Item1) / (YInterval.Item2 - YInterval.Item1) * Height);
            return new Tuple<uint, uint>(pointX, pointY);
        }
    }
}