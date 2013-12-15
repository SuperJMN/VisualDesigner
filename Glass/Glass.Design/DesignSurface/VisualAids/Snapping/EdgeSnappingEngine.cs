using System;
using System.Collections.Generic;
using System.Windows;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    public abstract class EdgeSnappingEngine : ISnappingEngine
    {
        public EdgeSnappingEngine()
        {
            HorizontalEdges = new List<double>();
            VerticalEdges = new List<double>();
        }

        protected List<double> HorizontalEdges { get; private set; }
        protected List<double> VerticalEdges { get; private set; }

        public Point SnapPoint(Point pointToSnap)
        {
            var snappedX = Snap(pointToSnap.X, HorizontalEdges);
            var snappedY = Snap(pointToSnap.Y, VerticalEdges);
            
            var snapResult = new Point(snappedX, snappedY);
            return snapResult;
        }

        private static double Snap(double pointToSnap, IEnumerable<double> edges)
        {
            double snappedX = 0;
            var snapped = false;
            var enumerator = edges.GetEnumerator();
            while (enumerator.MoveNext() && !snapped)
            {
                var horizontalEdge = enumerator.Current;

                snappedX = MathOperations.Snap(pointToSnap, horizontalEdge, 10);
                if (Math.Abs(snappedX - pointToSnap) > 0.1)
                {
                    snapped = true;
                }
            }
            return snappedX;
        }
    }
}