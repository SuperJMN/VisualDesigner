using System.Collections.Generic;
using System.Linq;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.CanvasItem
{
    public class Aligner
    {
        private readonly IList<ICanvasItem> canvasItems;

        public Aligner(IList<ICanvasItem> canvasItems)
        {
            this.canvasItems = canvasItems;
        }

        private IList<ICanvasItem> CanvasItems
        {
            get { return canvasItems; }
        }

        public void AlignLeft()
        {
            var minLeft = CanvasItems.Min(canvasItem => canvasItem.Left);
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Left = minLeft;
            }
        }

        public void AlignToRight()
        {
            var maxRight = CanvasItems.Max(item => item.Right);
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Left = maxRight - canvasItem.Width;
            }
        }

        public void AlignToTop()
        {
            var minTop = CanvasItems.Min(item => item.Top);
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Top = minTop;
            }
        }

        public void AlignToBottom()
        {
            var maxBottom = CanvasItems.Max(item => item.Bottom);
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Top = maxBottom - canvasItem.Height;
            }
        }

        public void AlignToMiddleHorizontal()
        {
            var minLeft = CanvasItems.Min(item => item.Left);
            var maxRight = CanvasItems.Max(item => item.Right);

            var middle = maxRight - minLeft;
            AdjustLeftsAround(middle);
        }

        private void AdjustTopsAround(double middle)
        {
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Top = middle - canvasItem.Height / 2;
            }
        }

        private void AdjustLeftsAround(double middle)
        {
            foreach (var canvasItem in CanvasItems)
            {
                canvasItem.Left = middle - canvasItem.Width / 2;
            }
        }

        public void AlignToMiddleVertical()
        {
            var minTop = CanvasItems.Min(item => item.Top);
            var maxBottom = CanvasItems.Max(item => item.Bottom);

            var middle = (maxBottom - minTop) / 2 + minTop;

            AdjustTopsAround(middle);
        }

        public void SetAlignEquallyHorizontal()
        {
            var totalSpace = GetTotalHorizontalSpaceBetween();
            var averageSpace = totalSpace / (CanvasItems.Count - 1);
            ApplyHorizontalSpace(averageSpace);
        }

        private void ApplyHorizontalSpace(double space)
        {
            var sortedList = GetSortedListByLeft();
            for (var i = 0; i < sortedList.Count - 1; i++)
            {
                var item1 = sortedList[i];
                var item2 = sortedList[i + 1];
                ApplySeparationBetween(item1, item2, space);
            }
        }

        private IList<ICanvasItem> GetSortedListByLeft()
        {
            var sortedList = new List<ICanvasItem>(CanvasItems);
            sortedList.Sort((item1, item2) => item1.Left.CompareTo(item2.Left));
            return sortedList;
        }

        private double GetTotalHorizontalSpaceBetween()
        {
            var totalSpace = 0D;
            var sortedList = GetSortedListByLeft();

            for (var i = 0; i < sortedList.Count - 1; i++)
            {
                var item1 = sortedList[i];
                var item2 = sortedList[i + 1];

                totalSpace += GetHorizontalSpaceBetween(item1, item2);
            }

            return totalSpace;
        }

        private static double GetHorizontalSpaceBetween(ICanvasItem item1, ICanvasItem item2)
        {
            return item2.Left - item1.Right;
        }

        private static void ApplySeparationBetween(ICanvasItem item1, ICanvasItem item2, double separation)
        {
            var currentSeparation = GetHorizontalSpaceBetween(item1, item2);
            var deltaSeparation = separation - currentSeparation;
            var offset = ServiceLocator.CoreTypesFactory.CreatePoint(deltaSeparation, 0);
            item2.Offset(offset);
        }

        
    }

}