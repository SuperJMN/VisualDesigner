using Windows.Foundation;

namespace Glass.Design.WinRT.Core
{
    public static class PointExtensions
    {
        public static Point Swap(this Point point)
        {
            return new Point(point.Y, point.X);
        }
    }
}