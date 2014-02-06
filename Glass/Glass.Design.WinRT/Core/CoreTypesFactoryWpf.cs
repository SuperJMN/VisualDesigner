using Windows.Foundation;

namespace Glass.Design.WinRT.Core
{
    public struct Vector
    {
        private readonly double x;
        private readonly double y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}