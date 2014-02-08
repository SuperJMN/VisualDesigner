using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.Canvas
{
    public interface ICanvasItem : IPositionable, ISizable, ICanvasItemContainer
    {
        double Right { get; set; }
        double Bottom { get; set; }

        ICanvasItemContainer Parent { get; set; }
    }
}