using System.Windows;
using Design.Interfaces;

namespace Glass.Design.CanvasItem
{
    public static class CanvasItemExtensions
    {
        public static Point GetLocation(this IPositionable positionable)
        {
            return new Point(positionable.Left, positionable.Top);
        }

        public static Point GetSize(this ISizable sizable)
        {
            return new Point(sizable.Width, sizable.Height);
        }

        public static void SetSize(this ISizable sizable, Size size)
        {
            sizable.Width = size.Width;
            sizable.Height = size.Height;                        
        }

        public static void SetLocation(this IPositionable positionable, Point location)
        {
            positionable.Left = location.X;
            positionable.Top = location.Y;
        }
    }
}