using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Domain.MathModule.Graphic
{
    public class GraphicConfig
    {
        public Size Size;
        public Tuple<double, double> XInterval;
        public Tuple<double, double> YInterval;
        public Func<double, double> Func { get; }
        public GraphicConfig(
            Size size, 
            Tuple<double, double> xInterval,
            Tuple<double, double> yInterval,
            Func<double, double> rawFunction)
        {
            Size = size;
            XInterval = xInterval;
            YInterval = yInterval;
            Func = rawFunction;
        }
    }
}
