using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public static class CanvasItemExtensions
    {
        public static IPoint GetLocation(this IPositionable positionable)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(positionable.Left, positionable.Top);
        }

        public static IPoint GetSize(this ISizable sizable)
        {
            return ServiceLocator.CoreTypesFactory.CreatePoint(sizable.Width, sizable.Height);
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
    }
}