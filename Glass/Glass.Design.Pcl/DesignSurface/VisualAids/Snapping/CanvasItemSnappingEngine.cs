using System.Collections.Generic;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class CanvasItemSnappingEngine : EdgeSnappingEngine, ICanvasItemSnappingEngine
    {
        public CanvasItemSnappingEngine(double threshold)
            : base(threshold)
        {

        }

        private IEnumerable<ICanvasItem> magnets;

        public IEnumerable<ICanvasItem> Magnets
        {
            get { return magnets; }
            set
            {
                magnets = value;
                GenerateEdges();
            }
        }

        private void GenerateEdges()
        {
            Edges.Clear();
            
            foreach (var canvasItem in Magnets)
            {
                AddHorizontalEdges(canvasItem);
                AddVerticalEdges(canvasItem);
            }
        }

        private void AddHorizontalEdges(ICanvasItem canvasItem)
        {
            var range = new Range(canvasItem.Left, canvasItem.Right);

            Edges.Add(new Edge(canvasItem.Top, range, Orientation.Horizontal));
            Edges.Add(new Edge(canvasItem.Top + canvasItem.Height, range, Orientation.Horizontal));
        }

        private void AddVerticalEdges(ICanvasItem canvasItem)
        {
            var range = new Range(canvasItem.Top, canvasItem.Top + canvasItem.Height);

            Edges.Add(new Edge(canvasItem.Left, range, Orientation.Vertical));
            Edges.Add(new Edge(canvasItem.Right, range, Orientation.Vertical));
        }
    }
}