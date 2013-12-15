using System.Collections.Generic;
using System.Windows;
using Glass.Design.CanvasItem;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    public interface ICanvasItemSnappingEngine : ISnappingEngine
    {
        IEnumerable<ICanvasItem> Items { get; set; }
    }
}