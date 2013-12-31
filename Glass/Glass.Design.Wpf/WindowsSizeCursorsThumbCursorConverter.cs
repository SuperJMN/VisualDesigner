using System;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Core;
using Glass.Design.Wpf.Converters;
using ImpromptuInterface;
using Point = System.Windows.Point;


namespace Glass.Design.Wpf
{
    public class WindowsSizeCursorsThumbCursorConverter : IThumbCursorConverter
    {

        public Cursor GetCursor(IRect handleRect, IRect parentRect)
        {
            var discretizedHandle = handleRect.MiddlePoint();
            var edgeSizeOfEquivalentSquare = parentRect.Size.EdgeOfEquivaletSquare();

            var x = Geometrics.LinearProportion(discretizedHandle.X, parentRect.Width, edgeSizeOfEquivalentSquare);
            var y = Geometrics.LinearProportion(discretizedHandle.Y, parentRect.Height, edgeSizeOfEquivalentSquare);

            var equivalentDiscretizedHandle = new Point(x, y);

            return GetCursorFromPointsInSquare(edgeSizeOfEquivalentSquare, equivalentDiscretizedHandle);
        }

        public static Cursor GetCursorFromPointsInSquare(double sideSize, Point end)
        {
            var halfSide = sideSize / 2;

            var center = new Point(halfSide, halfSide);

            var deg = Geometrics.GetDegress(center.ActLike<IPoint>(), end.ActLike<IPoint>());

            var segment = GetHotSpotSegment(deg);

            return CursorFromSegment(segment);
        }

        private static int GetHotSpotSegment(double deg)
        {
            const double totalDeg = 360D;
            const int numSegments = 8;
            var hotSpot = deg / totalDeg * numSegments;
            var roundedHotSpot = Math.Round(hotSpot);

            return (int)roundedHotSpot;
        }

        private static Cursor CursorFromSegment(int segment)
        {
            if (segment == 3 || segment == 7)
            {
                return Cursors.SizeNWSE;
            }
            if (segment == 2 || segment == 6)
            {
                return Cursors.SizeNS;
            }
            if (segment == 1 || segment == 5)
            {
                return Cursors.SizeNESW;
            }
            if (segment == 0 || segment == 4 || segment == 8)
            {
                return Cursors.SizeWE;
            }

            return Cursors.Arrow;
        }
    }
}