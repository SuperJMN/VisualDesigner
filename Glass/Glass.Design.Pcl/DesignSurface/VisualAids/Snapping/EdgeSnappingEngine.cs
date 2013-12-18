using System;
using System.Collections.Generic;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class EdgeSnappingEngine : SnappingEngine
    {
        public EdgeSnappingEngine()
        {
            HorizontalEdges = new List<Edge>();
            VerticalEdges = new List<Edge>();
        }

        protected List<Edge> HorizontalEdges { get; private set; }
        protected List<Edge> VerticalEdges { get; private set; }

        public override double SnapPoint(double value)
        {
            var snappedX = Snap(value, HorizontalEdges);
            return snappedX;
        }
       
        private static double Snap(double pointToSnap, IEnumerable<Edge> edges)
        {
            double snappedX = 0;
            var snapped = false;
            var enumerator = edges.GetEnumerator();
            while (enumerator.MoveNext() && !snapped)
            {
                var edge = enumerator.Current;

                snappedX = MathOperations.Snap(pointToSnap, edge.Origin, 10);
                if (Math.Abs(snappedX - pointToSnap) > 0.1)
                {
                    snapped = true;
                }
            }
            return snappedX;
        }
    }
}