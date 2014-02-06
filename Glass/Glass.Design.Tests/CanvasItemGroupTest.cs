using System.Collections.Generic;
using Glass.Design.Pcl.Canvas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CanvasItemGroupTest
    {
        private static CanvasItem Item1
        {
            get
            {

                var item = new CanvasModelItem
                           {
                               Left = 10,
                               Width = 120,
                               Top = 20,
                               Height = 30,
                           };

                return item;
            }
        }

        private static CanvasItem Item2
        {
            get
            {
                var item = new CanvasModelItem
                           {
                               Left = 40,
                               Width = 110,
                               Height = 20,                               
                               Top = 60,
                           };

                return item;
            }
        }

        private static List<ICanvasItem> Items
        {
            get
            {
                return new List<ICanvasItem>
                         {
                             Item1,
                             Item2,
                         };
            }
        }

        [TestMethod]
        public void GetLeftTest()
        {
            var group = new CanvasItemSelection(Items);

            Assert.AreEqual(10, group.Left);
        }

        [TestMethod]
        public void GetTopTest()
        {
            var group = new CanvasItemSelection(Items);

            Assert.AreEqual(20, group.Top);
        }

        [TestMethod]
        public void GetHeightTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemSelection(items);

            Assert.AreEqual(60, group.Height);
        }

        [TestMethod]
        public void GetWidthTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemSelection(items);

            Assert.AreEqual(140, group.Width);
        }

        [TestMethod]
        public void SetWidthTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemSelection(items);

            group.Width = 280;


            var item1 = items[0];
            var item2 = items[1];

            Assert.AreEqual(280, group.Width);
            Assert.AreEqual(240, item1.Width);
            Assert.AreEqual(220, item2.Width);
        }

        [TestMethod]
        public void SetHeightTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemSelection(items);

            group.Height = 120;


            var item1 = group.Children[0];
            var item2 = group.Children[1];

            Assert.AreEqual(120, group.Height);
            Assert.AreEqual(60, item1.Height);
            Assert.AreEqual(40, item2.Height);
        }

        [TestMethod]
        public void SetLeftTest()
        {
            var items = Items;

            var group = new CanvasItemSelection(items);
            group.Left = 20;

            var item1 = group.Children[0];
            var item2 = group.Children[1];

            Assert.AreEqual(20, group.Left);
            Assert.AreEqual(20, item1.Left);
            Assert.AreEqual(50, item2.Left);
        }

        [TestMethod]
        public void SetTopTest()
        {
            var items = Items;

            var group = new CanvasItemSelection(items);
            group.Top = 30;

            var item1 = group.Children[0];
            var item2 = group.Children[1];

            Assert.AreEqual(30, group.Top);
            Assert.AreEqual(30, item1.Top);
            Assert.AreEqual(70, item2.Top);
        }

        [TestMethod]
        public void ZeroItemsWidthGroupTest()
        {
            var items = new List<ICanvasItem>();

            var group = new CanvasItemSelection(items);
            Assert.AreEqual(double.NaN, group.Width);
        }

        [TestMethod]
        public void ZeroItemsHeightGroupTest()
        {
            var items = new List<ICanvasItem>();

            var group = new CanvasItemSelection(items);
            Assert.AreEqual(double.NaN, group.Height);
        }

        [TestMethod]
        public void ItemsWithZeroWidth()
        {
            var item = new CanvasModelItem() { Left = 0, Top = 0, Width = 0, Height = 0 };

            var items = new List<ICanvasItem>() { item };

            var group = new CanvasItemSelection(items);
            Assert.AreEqual(0, group.Width);
        }

        [TestMethod]
        public void ItemsWithZeroHeight()
        {
            var item = new CanvasModelItem() { Left = 0, Top = 0, Width = 0, Height = 0 };

            var items = new List<ICanvasItem>() { item };

            var group = new CanvasItemSelection(items);
            Assert.AreEqual(0, group.Height);
        }

    }
}
