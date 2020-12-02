using System;
using TelegramMathBot.Infrastructure.GraphicModule;
using SFML.Graphics;

namespace TelegramMathBot.Infrastructure.MathModule
{
    public static class GraphicSolver
    {
        /// <summary>
        /// Generates given function graphic and saves in into "filename".png
        /// </summary>
        /// <param name="screenWidth">Width of PNG Image</param>
        /// <param name="screenHeight">Height of PNG Image</param>
        /// <param name="filename">Name of file w/o extension</param>
        /// <param name="xInterval">Horizontal segment describing visible part of oX axis</param>
        /// <param name="yInterval">Vertical segment describing visible part of oY axis</param>
        /// <param name="rawFunction">Function, which will be drawn</param>
        public static System.Drawing.Bitmap Solve(int screenWidth, int screenHeight, string filename, Tuple<double, double> xInterval, Tuple<double, double> yInterval, Func<double, double> rawFunction)
        {
            var imgProcessor = new ImageProcessor((uint)screenWidth, (uint)screenHeight, filename, xInterval, yInterval);
            imgProcessor.DrawAxes();
            var processedFunction = new RealArgumentFunction(rawFunction);
            processedFunction.CalculatePointsInInterval(xInterval, yInterval);
            imgProcessor.DrawGraphic(processedFunction.GraphPoints, SFML.Graphics.Color.Green);
            
            return GetBitmapFromImage(imgProcessor.GraphImage);
        }

        private static System.Drawing.Bitmap GetBitmapFromImage(Image image)
        {
            var height = (int)image.Size.X;
            var width = (int)image.Size.Y;
            var res = new System.Drawing.Bitmap(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var pixel = image.GetPixel((uint)x, (uint)y);
                    var A = pixel.A;
                    var R = pixel.R;
                    var G = pixel.G;
                    var B = pixel.B;
                    var color = System.Drawing.Color.FromArgb(A, R, G, B);
                    res.SetPixel(x, y, color);
                }
            }

            return res;
        }
    }
}