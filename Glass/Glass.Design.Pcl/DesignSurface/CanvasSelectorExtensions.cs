using System.Collections.Generic;
using System.Linq;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.DesignSurface
{
    public static class CanvasSelectorExtensions
    {
        public static IEnumerable<ICanvasItem> GetSelectedCanvasItems(this ICanvasSelector canvasSelector)
        {
            return canvasSelector.SelectedItems.Cast<ICanvasItem>();
        }
    }
}