using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class EdgeSnappingEngine : SnappingEngine, IEdgeSnappingEngine
    {
        public EdgeSnappingEngine(double threshold)
            : base(threshold)
        {
            Edges = new List<Edge>();
          
            UnderlyingSnappedEdges = new ObservableCollection<Edge>();
            SnappedEdges = new ReadOnlyObservableCollection<Edge>(UnderlyingSnappedEdges);
        }

        protected List<Edge> Edges { get; private set; }
        

        public ReadOnlyObservableCollection<Edge> SnappedEdges { get; private set; }

        private ObservableCollection<Edge> UnderlyingSnappedEdges { get; set; }

        public override double SnapLeft(double original)
        {
            return Snap(original, Edges.Where(edge => edge.Orientation == Orientation.Vertical));
        }

        public override double SnapTop(double original)
        {
            return Snap(original, Edges.Where(edge => edge.Orientation == Orientation.Horizontal));
        }

        public double Snap(double original, IEnumerable<Edge> edges)
        {
            double snappedValue = original;
            bool alreadySnapped = false;

            var enumerator = edges.GetEnumerator();
            while (enumerator.MoveNext() && !alreadySnapped)
            {
                var currentEdge = enumerator.Current;

                snappedValue = MathOperations.Snap(original, currentEdge.AxisDistance, Threshold);

                alreadySnapped = ValueIsSnapped(original, snappedValue);

                var snapStatus = Math.Abs(currentEdge.AxisDistance - snappedValue) < 0.1;
                SynchronizeSnappedEdgesFor(currentEdge, snapStatus);
            }

            return snappedValue;
        }

        private void SynchronizeSnappedEdgesFor(Edge currentEdge, bool snapped)
        {
            if (UnderlyingSnappedEdges.Contains(currentEdge) && !snapped)
            {
                UnderlyingSnappedEdges.Remove(currentEdge);
            }
            else if (!UnderlyingSnappedEdges.Contains(currentEdge) && snapped)
            {
                UnderlyingSnappedEdges.Add(currentEdge);
            }
        }

        private static bool ValueIsSnapped(double original, double result)
        {
            return Math.Abs(result - original) > 0.1;
        }

        public void ClearSnappedEdges()
        {
            UnderlyingSnappedEdges.Clear();
        }
    }

    public interface IEdgeSnappingEngine : ISnappingEngine
    {
        void ClearSnappedEdges();
    }
}