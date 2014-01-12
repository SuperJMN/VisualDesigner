using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public interface ICanvasItem : IPositionable, ISizable, ICanvasItemParent
    {
        double Right { get; }
        double Bottom { get; }

        ICanvasItemParent Parent { get; set; }
    }

    public interface ICanvasItemParent
    {
        CanvasItemCollection Children { get; }
    }
}