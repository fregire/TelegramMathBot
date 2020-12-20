using System;
using Domain.GraphicModule;
using Domain.AdditionalMath;
using SFML.Graphics;

namespace Domain.MathModule.Graphic
{
    public class GraphicSolver: ISolver<GraphicConfig, Image>
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
        public Image Solve(GraphicConfig config)
        {
            var imgProcessor = new ImageProcessor(
                (uint)config.Size.Width, 
                (uint)config.Size.Height, 
                config.XInterval, 
                config.YInterval);
            imgProcessor.DrawAxes();
            var processedFunction = new RealArgumentFunction(config.Func);
            processedFunction.CalculatePointsInInterval(
                config.XInterval,
                config.YInterval);
            imgProcessor.DrawGraphic(processedFunction.GraphPoints, SFML.Graphics.Color.Green);
            
            return imgProcessor.GraphImage;
        }
    }
}