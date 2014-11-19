using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.Canvas
{
    public interface ICanvasItem : ICanvasItemContainer, IPositionable, ISizable, INamed
    {
        double Right { get; }
        double Bottom { get; }

        ICanvasItemContainer Parent { get; }
    }
}