using System.Collections.Generic;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class NoEffectsCanvasItemSnappingEngine : NoEffectsSnappingEngine, ICanvasItemSnappingEngine
    {
        public IEnumerable<ICanvasItem> Magnets { get; set; }
        public ICanvasItem Snappable { get; set; }
        public void ClearSnappedEdges()
        {
            
        }
    }
}