using System;
using System.Collections.Generic;
using System.Text;
using Domain.MathModule;
using NUnit.Framework;
using TelegramMathBot.View.Parsers;


namespace Tests
{
    class ExpressionSolverTests
    {

        [TestCase("2+6", "2+3!")]
        [TestCase("1+2+10-8", "1+2+10-8")]
        public void ExpressionParserTest(string expectedRes, string expression)
        {
            var parser = new ExpressionParser();
            Assert.AreEqual(expectedRes, parser.Parse(expression));
        }

        [TestCase(14, "2+2+10")]
        [TestCase(10, "2-2+10")]
        [TestCase(6, "2-2+3!")]
        [TestCase(62, "5*10+12")]
        [TestCase(72, "3!*10+12")]
        public void ExpressionSolverTest(decimal expectedRes, string expression)
        {
            var parser = new ExpressionParser();
            var solver = new ExpressionSolver();
            var parsedData = parser.Parse(expression);
            Assert.AreEqual(expectedRes, solver.Solve(parsedData));
        }
    }
}
