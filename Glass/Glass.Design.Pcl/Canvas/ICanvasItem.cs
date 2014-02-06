using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.Canvas
{
    public interface ICanvasItem : IPositionable, ISizable, ICanvasItemContainer
    {
        double Right { get; }
        double Bottom { get; }

        ICanvasItemContainer Parent { get; }
    }
}