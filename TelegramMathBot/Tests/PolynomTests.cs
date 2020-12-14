using Domain.MathModule;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class PolynomTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SolverTest()
        {
            var coeffs = new List<double> { 1, -1};
            var roots = PolynomialSolver.Solve(coeffs);

            Assert.AreEqual(roots[0], 1);
        }
    }
}