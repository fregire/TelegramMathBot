using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TelegramMathBot.GraphicModule
{ 
    public class GraphFunction
    {
        public Expression Function;
        public List<Tuple<double, double>> GraphPoints;
        
        public GraphFunction()
        {
            throw new NotImplementedException();
        }

        public double ValueAt(double x)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Graph
    {
        public IEnumerable<GraphFunction> Functions; 
        public Tuple<double, double> XInterval;
        public Tuple<double, double> YInterval;
        
        public Graph(Tuple<double, double> xInterval, Tuple<double, double> yInterval)
        {
            XInterval = xInterval;
            YInterval = yInterval;
            
            throw new NotImplementedException();
        }

        public void ProcessImageFromBitmap(string filename)
        {
            throw new NotImplementedException();
        }

        public void ChangeIntervalX()
        {
            throw new NotImplementedException();
        }
        
        public void ChangeIntervalY()
        {
            throw new NotImplementedException();
        }

        public void RecalculateAfterUpdatingInterval()
        {
            throw new NotImplementedException();
        }
    }
    
}