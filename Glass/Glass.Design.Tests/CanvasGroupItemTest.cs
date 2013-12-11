using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows.Controls;
using System.Windows.Documents;
using Design.Interfaces;
using Glass.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CanvasGroupItemTest
    {
        private static FrameworkElementToCanvasItemAdapter Item1
        {
            get
            {

                var item = new FrameworkElementToCanvasItemAdapter
                                                   {
                                                       Content = new Button
                                                                 {
                                                                     Content = new Run("Hola tío"),
                                                                 }
                                                       ,
                                                       Width = 120,
                                                       Height = 30,
                                                       Left = 10,
                                                       Top = 20,
                                                   };
                return item;
            }
        }

        private static FrameworkElementToCanvasItemAdapter Item2
        {
            get
            {
                var item = new FrameworkElementToCanvasItemAdapter
                                                   {
                                                       Content = new TextBlock
                                                                 {
                                                                     Text = "Hola tío",

                                                                 },
                                                       Width = 110,
                                                       Height = 20,
                                                       Left = 40,
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
            var group = new CanvasItemGroup(Items);

            Assert.AreEqual(group.Left, 10);
        }

        [TestMethod]
        public void GetTopTest()
        {
            var group = new CanvasItemGroup(Items);

            Assert.AreEqual(group.Top, 20);
        }

        [TestMethod]
        public void GetHeightTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemGroup(items);

            Assert.AreEqual(group.Height, 60);
        }

        [TestMethod]
        public void GetWidthTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemGroup(items);

            Assert.AreEqual(group.Width, 140);
        }

        [TestMethod]
        public void SetWidthTest()
        {
            var items = new List<ICanvasItem>
                        {
                            Item1,
                            Item2,
                        };

            var group = new CanvasItemGroup(items);

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

            var group = new CanvasItemGroup(items);

            group.Height = 120;


            var item1 = group[0];
            var item2 = group[1];

            Assert.AreEqual(120, group.Height);
            Assert.AreEqual(60, item1.Height);
            Assert.AreEqual(40, item2.Height);
        }

        [TestMethod]
        public void SetLeftTest()
        {
            var items = Items;

            var group = new CanvasItemGroup(items);
            group.Left = 20;

            var item1 = group[0];
            var item2 = group[1];

            Assert.AreEqual(20, group.Left);
            Assert.AreEqual(20, item1.Left);
            Assert.AreEqual(50, item2.Left);
        }

        [TestMethod]
        public void SetTopTest()
        {
            var items = Items;

            var group = new CanvasItemGroup(items);
            group.Top = 30;

            var item1 = group[0];
            var item2 = group[1];

            Assert.AreEqual(30, group.Top);
            Assert.AreEqual(30, item1.Top);
            Assert.AreEqual(70, item2.Top);
        }

        [TestMethod]
        public void ZeroItemsWidthGroupTest()
        {
            var items = new List<ICanvasItem>();

            var group = new CanvasItemGroup(items);
            Assert.AreEqual(double.NaN, group.Width);
        }

        [TestMethod]
        public void ZeroItemsHeightGroupTest()
        {
            var items = new List<ICanvasItem>();

            var group = new CanvasItemGroup(items);
            Assert.AreEqual(double.NaN, group.Height);
        }

        [TestMethod]
        public void ItemsWithZeroWidth()
        {
            var item = new FrameworkElementToCanvasItemAdapter() { Left = 0, Top = 0, Width = 0, Height = 0 };
            item.Content = new Button() { Width = 30, Height = 10 };

            var items = new List<ICanvasItem>() { item };

            var group = new CanvasItemGroup(items);
            Assert.AreEqual(0, group.Width);
        }

        [TestMethod]
        public void ItemsWithZeroHeight()
        {
            var item = new FrameworkElementToCanvasItemAdapter() { Left = 0, Top = 0, Width = 0, Height = 0 };
            item.Content = new Button() { Width = 30, Height = 10 };

            var items = new List<ICanvasItem>() { item };

            var group = new CanvasItemGroup(items);
            Assert.AreEqual(0, group.Height);
        }

    }
}
