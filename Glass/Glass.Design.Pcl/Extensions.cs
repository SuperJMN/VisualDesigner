using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl
{
    public static class Extensions
    {
       
        private static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static IRect Rect(this ICanvasItem item)
        {
            return new Rect(item.Left, item.Top, item.Width, item.Height);
        }

        public static IPoint MiddlePoint(this IRect item)
        {
            return new Point(item.Left + item.Width / 2, item.Top + item.Height / 2);
        }

        public static IPoint GetLocation(this IPositionable positionable)
        {
            return new Point(positionable.Left, positionable.Top);
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

        public static void SetLocation(this IPositionable positionable, double x, double y)
        {
            positionable.Left = x;
            positionable.Top = y;
        }

        public static IPoint Subtract(this IPoint point, IVector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y);
        }

        public static IPoint Subtract(this IPoint point, IPoint vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y);
        }

        public static IPoint Add(this IPoint point, IPoint vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y);
        }


        public static IPoint Add(this IPoint point, IVector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y);
        }

        public static IPoint Swap(this IPoint point)
        {
            return new Point(point.Y, point.X);
        }


        public static IPoint GetOpposite(this IPoint handlePosition, IPoint middlePoint)
        {
            var x = 2 * middlePoint.X - handlePosition.X;
            var y = 2 * middlePoint.Y - handlePosition.Y;

            return new Point(x, y);
        }

        public static IPoint FromParentToLocal(this IPoint origin, IPoint destination)
        {
            var x = origin.X - destination.X;
            var y = origin.Y - destination.Y;
            return new Point(x, y);
        }

        public static IPoint FromLocalToParent(this IPoint origin, IPoint destination)
        {
            var x = origin.X + destination.X;
            var y = origin.Y + destination.Y;
            return new Point(x, y);
        }

        public static Range Swap(this Range range)
        {
            return new Range(range.SegmentEnd, range.SegmentStart);
        }

        public static IPoint GetHandlePoint(this IRect rect, ISize parentSize)
        {
            var middleThumb = rect.MiddlePoint();

            var propX = MathOperations.SquareRounding(middleThumb.X, parentSize.Width, 3) / 3D;
            var propY = MathOperations.SquareRounding(middleThumb.Y, parentSize.Height, 3) / 3D;

            return new Point(propX, propY);
        }

        public static IVector ToVector(this ISize size)
        {
            return ServiceLocator.CoreTypesFactory.CreateVector(size.Width, size.Height);
        }

        public static IVector ToVector(this IPoint point)
        {
            return ServiceLocator.CoreTypesFactory.CreateVector(point.X, point.Y);
        }

        public static ISize ToSize(this IVector vector)
        {
            return ServiceLocator.CoreTypesFactory.CreateSize(vector.X, vector.Y);
        }

        public static IVector Subtract(this IVector vector, IVector another)
        {
            return ServiceLocator.CoreTypesFactory.CreateVector(vector.X - another.X, vector.Y - another.Y);
        }

        public static IVector Add(this IVector vector, IVector another)
        {
            return ServiceLocator.CoreTypesFactory.CreateVector(vector.X + another.X, vector.Y + another.Y);
        }

        public static void SetBounds(this ICanvasItem canvasItem, IRect rect)
        {
            canvasItem.SetLocation(rect.Left, rect.Top);
            canvasItem.SetSize(rect.Size);
        }

        public static void SetLeftKeepingRight(this IRect rect, double left)
        {
            var right = rect.Right;
            rect.X = left;
            rect.Width = right - left;
        }

        public static void SetTopKeepingBottom(this IRect rect, double top)
        {
            var bottom = rect.Bottom;
            rect.Y = top;
            rect.Height = bottom - top;
        }

        public static void SetRightKeepingLeft(this IRect rect, double right)
        {
            rect.Width = right - rect.Left;
        }

        public static void SetBottomKeepingTop(this IRect rect, double bottom)
        {
            rect.Height = bottom - rect.Top;
        }

        public static IRect GetBoundsFromChildren(IEnumerable<ICanvasItem> items)
        {
            var left = items.Min(item => item.Left);
            var top = items.Min(item => item.Top);
            var right = items.Max(item => item.Right);
            var bottom = items.Max(item => item.Bottom);

            return new Rect(left, top, right - left, bottom - top);
        }

        public static void Offset(this ICanvasItem canvasItem, IPoint point)
        {
            canvasItem.Left += point.X;
            canvasItem.Top += point.Y;
        }

        public static void Offset(this IRect canvasItem, IPoint point)
        {
            canvasItem.X += point.X;
            canvasItem.Y += point.Y;
        }

        public static IPoint Negative(this IPoint point)
        {
            return new Point(-point.X, -point.Y);
        }

        public static IRect GetBoundingRect(IList<ICoordinate> children)
        {

            var left = GetLeft(children);
            var top = GetTop(children);
            var width = GetWidth(children);
            var height = GetHeight(children);

            return new Rect(left, top, width, height);
        }

        public static double GetWidth(this IEnumerable<ICoordinate> children)
        {
            return children.GetMaxCoordinate(CoordinatePart.Right) - children.GetLeft();
        }

        public static double GetTop(this IEnumerable<ICoordinate> children)
        {
            return children.GetMinCoordinate(CoordinatePart.Top);
        }

        public static double GetLeft(this IEnumerable<ICoordinate> children)
        {
            return children.GetMinCoordinate(CoordinatePart.Left);
        }

        public static double GetHeight(this IEnumerable<ICoordinate> children)
        {

            return children.GetMaxCoordinate(CoordinatePart.Bottom) - children.GetTop();
        }

        public static double GetMaxCoordinate(this IEnumerable<ICoordinate> items, CoordinatePart part)
        {
            if (!items.Any())
            {
                return Double.NaN;
            }
            var right = items.Max(item => item.GetCoordinate(part));
            return right;
        }

        public static double GetMinCoordinate(this IEnumerable<ICoordinate> items, CoordinatePart part)
        {
            if (!items.Any())
            {
                return Double.NaN;
            }
            var right = items.Min(item => item.GetCoordinate(part));
            return right;
        }
    }
}