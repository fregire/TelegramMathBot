using Domain.MathModule;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace Tests
{
    public class PolynomTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void OneRootTest()
        {
            var expectedRoot = 1;
            var roots = PolynomialSolver.Solve(new List<double> { 1, -1 });

            Assert.AreEqual(roots.Count, 1);
            Assert.AreEqual(roots.First(), expectedRoot);
        }

        [Test]
        public void SquareEquationTest()
        {
            var expectedRoots = new List<double> { 2, 6 };
            var roots = PolynomialSolver.Solve(new List<double> { 1, -8, 12 });

            CollectionAssert.AreEquivalent(roots, expectedRoots);
        }

        [Test]
        public void WithoutOneCoeffTest()
        {
            var expectedRoots = new List<double> { 2, -2 };
            var roots = PolynomialSolver.Solve(new List<double> { 1, 0, -4});

            CollectionAssert.AreEquivalent(roots, expectedRoots);
        }

        [Test]
        public void WithoutRootsTest()
        {
            var roots = PolynomialSolver.Solve(new List<double> { 1, 0, 4 });

            CollectionAssert.IsEmpty(roots);
        }
    }
}