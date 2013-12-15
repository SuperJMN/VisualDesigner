using System.Collections.Generic;
using Glass.Design.CanvasItem;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    class NoEffectsCanvasItemSnappingEngine : NoEffectsSnappingEngine, ICanvasItemSnappingEngine
    {
        public IEnumerable<ICanvasItem> Items { get; set; }
    }
}