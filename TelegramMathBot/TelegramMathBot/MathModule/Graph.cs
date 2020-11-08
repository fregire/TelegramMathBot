using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TelegramMathBot.MathModule
{
    public enum Color
    {
        Red,
        Orange,
        Yellow,
        Green,
        Cyan,
        Blue,
        Violet,
        Black
    }

    public class GraphFunction
    {
        public Color color;
        public Expression function;
        
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
        public Tuple<double, double> X_interval;
        public Tuple<double, double> Y_interval;
        public Tuple<int, int> screenResolution;
        private List<Tuple<double, double>> GraphPoints;

        public Graph()
        {
            throw new NotImplementedException();
        }

        public void ProcessImage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<int, int>> TransformPointToImage(Tuple<double, double> position)
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

        public void RecalculateAfterUpdating()
        {
            throw new NotImplementedException();
        }
    }
    
}