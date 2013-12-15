using System.Collections.Generic;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class CanvasItemSnappingEngine : EdgeSnappingEngine, ICanvasItemSnappingEngine
    {
        public CanvasItemSnappingEngine()
        {
           
        }

        private IEnumerable<ICanvasItem> items;

        public IEnumerable<ICanvasItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                GenerateEdges();
            }
        }

        private void GenerateEdges()
        {
            HorizontalEdges.Clear();
            VerticalEdges.Clear();

            foreach (var canvasItem in Items)
            {
                HorizontalEdges.Add(canvasItem.Left);
                HorizontalEdges.Add(canvasItem.Left + canvasItem.Width);
                VerticalEdges.Add(canvasItem.Left);
                VerticalEdges.Add(canvasItem.Top + canvasItem.Height);
            }
        }
    }
}