using System.Windows;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.Core;
using ImpromptuInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HorizontalAlignment = Glass.Design.Pcl.DesignSurface.VisualAids.Resize.HorizontalAlignment;
using VerticalAlignment = Glass.Design.Pcl.DesignSurface.VisualAids.Resize.VerticalAlignment;

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
            var canvasItem = new CanvasItem
                             {
                                 Width = 30,
                                 Height = 30,
                                 Left = 10,
                                 Top = 20,
                             };

            var hookPoint = new Point(40, 20).ActLike<IPoint>();
            var newPoint = new Point(50, 20).ActLike<IPoint>();

            var resizeOperation = new ResizeOperation(canvasItem, new ResizeHandle
                                                                  {
                                                                      HorizontalAlignment = HorizontalAlignment.Right,
                                                                      VerticalAlignment = VerticalAlignment.Top,
                                                                  }, new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(40D, canvasItem.Width);
        }

    }
}