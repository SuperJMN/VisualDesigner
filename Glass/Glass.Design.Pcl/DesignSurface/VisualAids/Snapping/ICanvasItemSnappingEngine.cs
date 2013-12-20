using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ICanvasItemSnappingEngine : IEdgeSnappingEngine
    {
        IEnumerable<ICanvasItem> Magnets { get; set; }
    }
}