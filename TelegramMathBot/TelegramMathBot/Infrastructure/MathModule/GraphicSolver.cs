using System;
using TelegramMathBot.Infrastructure.GraphicModule;

namespace TelegramMathBot.Infrastructure.MathModule
{
    public class GraphicSolver
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
        public static void Solve(int screenWidth, int screenHeight, string filename, Tuple<double, double> xInterval, Tuple<double, double> yInterval, Func<double, double> rawFunction)
        {
            var imgProcessor = new ImageProcessor((uint)screenWidth, (uint)screenHeight, filename, xInterval, yInterval);
            imgProcessor.DrawAxes();
            var processedFunction = new RealArgumentFunction(rawFunction);
            processedFunction.CalculatePointsInInterval(xInterval, yInterval);
            imgProcessor.DrawGraphic(processedFunction.GraphPoints, SFML.Graphics.Color.Green);
            while (!imgProcessor.SaveFile()) {}
        }
    }
}