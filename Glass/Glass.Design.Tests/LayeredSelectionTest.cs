using System;
using Glass.Design.Selection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class LayeredSelectionTest
    {
        [TestMethod]
        public void AddItem()
        {
            var selectionGrouper = new LayeredSelection<string>();

            var item = "Hola";

            selectionGrouper.Add(item, 0);
            Assert.AreEqual(true, selectionGrouper.Contains(item));
        }

        [TestMethod]
        public void CreateSelection()
        {
            var selectionGrouper = new LayeredSelection<string>();

            var item1 = "Hola";
            var item2 = "Hola2";

            selectionGrouper.Add(item1, 4);
            selectionGrouper.Add(item2, 6);

            var selection = selectionGrouper.GetEffectiveSelection();
            Assert.AreEqual(1, selection.Count);
        }
    }
}
