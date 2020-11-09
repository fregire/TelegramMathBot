using System;
using System.Collections.Generic;
using TelegramMathBot.Infrastructure.MathModule;

namespace TelegramMathBot.Infrastructure.GraphicModule
{ 
    public class GraphicProcessor
    {
        public List<RealArgumentFunction> Functions; 
        public Tuple<double, double> XInterval;
        public Tuple<double, double> YInterval;
        
        public GraphicProcessor(Tuple<double, double> xInterval, Tuple<double, double> yInterval)
        {
            XInterval = xInterval;
            YInterval = yInterval;
            Functions = new List<RealArgumentFunction>();            
        }
        
        public void ChangeIntervalX()
        {
            throw new NotImplementedException();
        }
        
        public void ChangeIntervalY()
        {
            throw new NotImplementedException();
        }

        public void CalculatePoints()
        {
            foreach (var function in Functions)
                function.CalculatePointsInInterval(XInterval, YInterval);
        }
    }
    
}