using System.Windows;
using Glass.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void SlopeTestZero()
        {
            var p = Geometrics.Slope(new Point(4, 9), new Point(10, 9));
            Assert.AreEqual(0, p);
        }

        [TestMethod]
        public void SlopeTest1()
        {
            var p = Geometrics.Slope(new Point(1, 3), new Point(2, 4));
            Assert.AreEqual(1, p);
        }

        [TestMethod]
        public void SlopeTest2()
        {
            var p = Geometrics.Slope(new Point(1, 3), new Point(2, 2));
            Assert.AreEqual(-1, p);
        }

        [TestMethod]
        public void SlopeInfinity()
        {
            var p = Geometrics.Slope(new Point(1, 3), new Point(1, 7));
            Assert.AreEqual(true, double.IsInfinity(p));
        }

        [TestMethod]
        public void OppositeTest1()
        {
            var point = new Point(1, 0);
            var rect = new Rect(2, 0, 4, 5);
            var opposite = Geometrics.GetOpposite(point, rect);
            Assert.AreEqual(new Point(7, 5), opposite);
        }

        [TestMethod]
        public void OppositeTest2()
        {
            var point = new Point(6, 4);
            var rect = new Rect(2, 1, 3, 3);
            var opposite = Geometrics.GetOpposite(point, rect);
            Assert.AreEqual(new Point(1, 1), opposite);
        }

        [TestMethod]
        public void OppositeTest3()
        {
            var point = new Point(5, 5);
            var rect = new Rect(0, 0, 5, 5);
            var opposite = Geometrics.GetOpposite(point, rect);
            Assert.AreEqual(new Point(0, 0), opposite);
        }

        [TestMethod]
        public void OppositeTest4()
        {
            var point = new Point(5, 0);
            var rect = new Rect(0, 0, 5, 5);
            var opposite = Geometrics.GetOpposite(point, rect);
            Assert.AreEqual(new Point(0, 5), opposite);
        }

        [TestMethod]
        public void OppositeTest5()
        {
            var point = new Point(0, 5);
            var rect = new Rect(0, 0, 5, 5);
            var opposite = Geometrics.GetOpposite(point, rect);
            Assert.AreEqual(new Point(5, 0), opposite);
        }

        [TestMethod]
        public void GetDegressUpperLeft()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(0, 0));
            Assert.AreEqual(135, degrees, 3);
        }

        [TestMethod]
        public void GetDegressUpperMiddle()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(50, 0));
            Assert.AreEqual(90, degrees, 3);
        }

        [TestMethod]
        public void GetDegressUpperRight()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(100, 0));
            Assert.AreEqual(45, degrees, 3);
        }

        [TestMethod]
        public void GetDegressMiddleLeft()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(0, 50));
            Assert.AreEqual(180, degrees, 3);
        }

        [TestMethod]
        public void GetDegressMiddleRight()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(100, 50));
            Assert.AreEqual(0, degrees, 3);
        }

        [TestMethod]
        public void GetDegressBottomLeft()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(0, 100));
            Assert.AreEqual(225, degrees, 3);
        }

        [TestMethod]
        public void GetDegressBottomMiddle()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(50, 100));
            Assert.AreEqual(270, degrees, 3);
        }

        [TestMethod]
        public void GetDegressBottomRight()
        {
            var degrees = Geometrics.GetDegress(new Point(50, 50), new Point(100, 100));
            Assert.AreEqual(315, degrees, 3);
        }
     
    }
}