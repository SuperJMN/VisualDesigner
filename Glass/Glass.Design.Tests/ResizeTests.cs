using System.Windows;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.Core;
using ImpromptuInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ResizeTests
    {

        static ResizeTests()
        {
            ServiceLocator.CoreTypesFactory = new CoreTypesFactoryWpf();
        }

        [TestMethod]
        public void IncreaseWidthTopRight()
        {
            var canvasItem = new CanvasItem()
                             {
                                 Width = 30,
                                 Height = 30,
                                 Left = 10,
                                 Top = 20,
                             };

            var hookPoint = new Point(40, 20).ActLike<IPoint>();
            var newPoint = new Point(50, 20).ActLike<IPoint>();

            var resizeOperation = new ResizeOperation(canvasItem, hookPoint, new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(40D, canvasItem.Width);
        }

        [TestMethod]
        public void IncreaseWidthTopLeft()
        {
            var canvasItem = new CanvasItem()
            {
                Width = 30,
                Height = 30,
                Left = 10,
                Top = 20,
            };

            var hookPoint = new Point(10, 0).ActLike<IPoint>();
            var newPoint = new Point(0, 0).ActLike<IPoint>();

            var resizeOperation = new ResizeOperation(canvasItem, hookPoint, new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(40D, canvasItem.Width);
        }

        [TestMethod]
        public void IncreaseHeightTopLeft()
        {
            var canvasItem = new CanvasItem()
            {
                Width = 30,
                Height = 30,
                Left = 10,
                Top = 20,
            };

            var hookPoint = new Point(10, 50).ActLike<IPoint>();
            var newPoint = new Point(10, 65).ActLike<IPoint>();

            var resizeOperation = new ResizeOperation(canvasItem, hookPoint, new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(45D, canvasItem.Height);
        }

        [TestMethod]
        public void IncreaseHeightBottomRight()
        {
            var canvasItem = new CanvasItem()
            {
                Width = 30,
                Height = 30,
                Left = 10,
                Top = 20,
            };

            var hookPoint = new Point(40, 50).ActLike<IPoint>();
            var newPoint = new Point(60, 70).ActLike<IPoint>();

            var resizeOperation = new ResizeOperation(canvasItem, hookPoint, new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(50D, canvasItem.Height);
        }
    }
}