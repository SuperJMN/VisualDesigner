using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{

    [TestClass]
    public class SnappingTest
    {
        public SnappingTest()
        {
            ServiceLocator.CoreTypesFactory = new CoreTypesFactoryWpf();
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var snappingEngine = new CanvasItemSnappingEngine(8);
            var canvasItems = new List<CanvasItem>
                              {
                                  new CanvasItem
                                  {
                                      Left = 10,
                                      Top = 10,
                                      Height = 20,
                                      Width = 20,
                                  },
                                  new CanvasItem
                                  {
                                      Left = 33,
                                      Top = 10,
                                      Height = 20,
                                      Width = 20,
                                  }
                              };


            snappingEngine.Magnets = canvasItems;
        }
    }
}