using Glass.Design.Pcl;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void SwapSingleTest()
        {
            var canvasItem = ServiceLocator.CoreTypesFactory.CreateRect(1, 2, 3, 4);
                             
            canvasItem.SwapCoordinates();

            Assert.AreEqual(2, canvasItem.Left);
            Assert.AreEqual(1, canvasItem.Top);
            Assert.AreEqual(4, canvasItem.Width);
            Assert.AreEqual(3, canvasItem.Height);
        }
         
    }
    
}