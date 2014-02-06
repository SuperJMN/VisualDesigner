using System.Collections.Generic;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ICanvasItemSnappingEngine : IEdgeSnappingEngine
    {
        IEnumerable<ICanvasItem> Magnets { get; set; }
    }
}