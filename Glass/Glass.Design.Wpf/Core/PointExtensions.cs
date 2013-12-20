using System.Windows;

namespace Glass.Design.Wpf.Core
{
    public static class PointExtensions
    {
        public static Point Swap(this Point point)
        {
            return new Point(point.Y, point.X);
        }
    }
}