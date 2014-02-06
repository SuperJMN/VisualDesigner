using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Point = Glass.Design.Pcl.Core.Point;

namespace UnitTestProject1
{
    [TestClass]
    public class ResizeTests
    {
        [TestMethod]
        public void IncreaseWidthTopRight()
        {
            var canvasItem = new CanvasModelItem
                             {
                                 Width = 30,
                                 Height = 30,
                                 Left = 10,
                                 Top = 20,
                             };

            var hookPoint = new Point(40, 20);
            var newPoint = new Point(50, 20);

            var resizeOperation = new ResizeOperation(canvasItem, newPoint , new NoEffectsCanvasItemSnappingEngine());
            resizeOperation.UpdateHandlePosition(newPoint);
            Assert.AreEqual(40D, canvasItem.Width);
        }

    }
}