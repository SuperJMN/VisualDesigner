using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;

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

        public static IPoint GetLocation(this IPositionable positionable)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(positionable.Left, positionable.Top);
        }

        public static ISize GetSize(this ISizable sizable)
        {
            return ServiceLocator.CoreTypesFactory.CreateSize(sizable.Width, sizable.Height);
        }

        public static void SetSize(this ISizable sizable, ISize size)
        {
            sizable.Width = size.Width;
            sizable.Height = size.Height;
        }

        public static void SetLocation(this IPositionable positionable, IPoint location)
        {
            positionable.Left = location.X;
            positionable.Top = location.Y;
        }

        public static IPoint Subtract(this IPoint point, IVector vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X - vector.X, point.Y - vector.Y);
        }

        public static IPoint Subtract(this IPoint point, IPoint vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X - vector.X, point.Y - vector.Y);
        }

        public static IPoint Add(this IPoint point, IPoint vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X + vector.X, point.Y + vector.Y);
        }

        public static IPoint Add(this IPoint point, IVector vector)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.X + vector.X, point.Y + vector.Y);
        }

        public static IPoint Swap(this IPoint point)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(point.Y, point.X);
        }

        public static IPoint GetMiddlePoint(this IRect rect)
        {
            var x = rect.Left + rect.Width / 2;
            var y = rect.Top + rect.Height / 2;
            return ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
        }

        public static IPoint GetOpposite(this IPoint handlePosition, IPoint middlePoint)
        {
            var x = 2 * middlePoint.X - handlePosition.X;
            var y = 2 * middlePoint.Y - handlePosition.Y;

            return ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
        }

        public static IPoint FromParentToLocal(this IPoint origin, IPoint destination)
        {
            var x = origin.X - destination.X;
            var y = origin.Y - destination.Y;
            return ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
        }

        public static Range Swap(this Range range)
        {
            return new Range(range.SegmentEnd, range.SegmentStart);
        }
    }
}