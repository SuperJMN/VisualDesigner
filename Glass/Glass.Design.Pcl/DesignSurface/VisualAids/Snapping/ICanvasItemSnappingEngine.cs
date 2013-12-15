using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ICanvasItemSnappingEngine : ISnappingEngine
    {
        IEnumerable<ICanvasItem> Items { get; set; }
    }
}