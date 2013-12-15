using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl
{
    public static class Extensions
    {       
        public static void SwapCoordinates(this IEnumerable<ICanvasItem> items)
        {
            foreach (var canvasItem in items)
            {
                canvasItem.SwapCoordinates();
            }
        }

        public static void SwapCoordinates(this ICanvasItem item)
        {
            var left = item.Left;
            var top = item.Top;
            Swap(ref left, ref top);
            var width = item.Width;
            var height = item.Height;
            Swap(ref width, ref height);

            item.Left = left;
            item.Top = top;
            item.Width = width;
            item.Height = height;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static IRect ToRect(this ICanvasItem item)
        {
            return ServiceLocator.CoreTypesFactory.CreateRect(item.Left, item.Top, item.Width, item.Height);
        }

        public static IPoint DiscretizeUsingAverage(this IRect item)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(item.Left + item.Width / 2, item.Top + item.Height / 2);
        }
    }
}