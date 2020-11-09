using System;
using System.Collections.Generic;
using System.Text;
using Flee;
using Flee.PublicTypes;

namespace TelegramMathBot.View.Parsers
{
    public class GraphicParser
    {
        public static Func<double, double> Parse(string input)
        {
            ExpressionContext context = new ExpressionContext();
            // Define some variables
            context.Variables["x"] = 0.0d;

            // Use the variables in the expression
            IDynamicExpression e = context.CompileDynamic(input);


            Func<double, double> expressionEvaluator = (double input) =>
            {
                context.Variables["x"] = input;
                var result = (double)e.Evaluate();
                return result;
            };

            return expressionEvaluator;
        }
    }
}
