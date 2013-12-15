using System;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl
{
    public static class Geometrics
    {
        public static double GetDegress(IPoint origin, IPoint destination)
        {
            var p = new PointCollection { destination };
            p.Offset(-origin.X, -origin.Y);

            var finalDestination = p[0];
            
            var x = finalDestination.X;
            var y = finalDestination.Y;

            var rad = Math.Atan2(-y, x);
            var degrees = RadianToDegree(rad);

            if (degrees < 0)
            {
                degrees += 360;
            }


            return degrees;
        }

        public static double EdgeOfEquivaletSquare(this ISize original)
        {
            var edgeSize = Math.Sqrt(original.Width*original.Height);
            return edgeSize;
        }

        public static IPoint GetOpposite(IPoint a, IRect rect)
        {
            var halfPointX = rect.Left + rect.Width / 2;
            var distanceX = halfPointX - a.X;
            var x = halfPointX + distanceX;

            var halfPointY = rect.Top + rect.Height / 2;
            var distanceY = halfPointY - a.Y;
            var y = halfPointY + distanceY;

            var opposite = ServiceLocator.CoreTypesFactory.CreatePoint(x, y);

            return opposite;
        }

        public static double Slope(IPoint start, IPoint end)
        {
            var rise = end.Y - start.Y;
            var run = end.X - start.X;

            var slope = rise / run;
            return slope;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static double LinearProportion(double oldValue, double oldTotal, double newTotal)
        {
            return oldValue / oldTotal * newTotal;
        }
    }
}