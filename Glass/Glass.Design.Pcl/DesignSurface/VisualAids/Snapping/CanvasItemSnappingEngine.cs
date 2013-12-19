using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Glass.Design.Pcl.CanvasItem;
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

        public EventHandler ItemSnapped;
        private ICanvasItem snappable;

        public IEnumerable<ICanvasItem> Magnets
        {
            get { return magnets; }
            set
            {
                magnets = value;
                GenerateEdges();
            }
        }

        public IEnumerable<Edge> GetSnappingEdges(IRect rect)
        {
            var snappingEdges = from horizontalEdge in HorizontalEdges
                                where ShouldSnap(horizontalEdge, rect.Left)
                                select horizontalEdge;

            return snappingEdges;
        }

        private void GenerateEdges()
        {
            HorizontalEdges.Clear();
            VerticalEdges.Clear();

            foreach (var canvasItem in Magnets)
            {
                HorizontalEdges.Add(new Edge(canvasItem.Left, canvasItem.Height));
                HorizontalEdges.Add(new Edge(canvasItem.Left + canvasItem.Width, canvasItem.Height));
                VerticalEdges.Add(new Edge(canvasItem.Left, canvasItem.Width));
                VerticalEdges.Add(new Edge(canvasItem.Top + canvasItem.Height, canvasItem.Width));
            }
        }
    }
}