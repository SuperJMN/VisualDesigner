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
            double snappedX1 = 0;
            var snapped = false;
            var enumerator = ((IEnumerable<Edge>) HorizontalEdges).GetEnumerator();
            while (enumerator.MoveNext() && !snapped)
            {
                var edge = enumerator.Current;

                snappedX1 = MathOperations.Snap(value, edge.Origin, 20);
                if (Math.Abs(snappedX1 - value) > 0.1)
                {
                    snapped = true;
                }
            }
            var snappedX = snappedX1;
            return snappedX;
        }
    }
}