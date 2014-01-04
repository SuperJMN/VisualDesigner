using System.Collections.ObjectModel;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public interface ICanvasItem : IPositionable, ISizable
    {
        double Right { get; }
        double Bottom { get; }

        CanvasItemCollection Children { get; }
    }
}