using System.Collections.Generic;
using System.Windows;
using Design.Interfaces;

namespace Glass.Design
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

        public static Rect ToRect(this ICanvasItem item)
        {
            return new Rect(item.Left, item.Top, item.Width, item.Height);
        }

        public static Point DiscretizeUsingAverage(this Rect item)
        {
            return new Point(item.Left + item.Width / 2, item.Top + item.Height / 2);
        }
    }
}