﻿#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Glass.Design.Pcl.Primitives;

#endregion

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class EdgeSnappingEngine : SnappingEngine, IEdgeSnappingEngine
    {
        protected EdgeSnappingEngine(double threshold)
            : base(threshold)
        {
            Edges = new List<Edge>();

            UnderlyingSnappedEdges = new ObservableCollection<Edge>();
            SnappedEdges = new ReadOnlyObservableCollection<Edge>(UnderlyingSnappedEdges);
        }

        protected List<Edge> Edges { get; private set; }


        public ReadOnlyObservableCollection<Edge> SnappedEdges { get; private set; }

        private ObservableCollection<Edge> UnderlyingSnappedEdges { get; set; }

        private IEnumerable<Edge> VerticalEdges
        {
            get { return Edges.Where(edge => edge.Orientation == Orientation.Vertical); }
        }

        private IEnumerable<Edge> HorizontalEdges
        {
            get { return Edges.Where(edge => edge.Orientation == Orientation.Horizontal); }
        }

        public override double SnapHorizontal(double original)
        {
            return Snap(original, VerticalEdges);
        }

        public override double SnapVertical(double original)
        {
            return Snap(original, HorizontalEdges);
        }

        public void ClearSnappedEdges()
        {
            UnderlyingSnappedEdges.Clear();
        }

        private double Snap(double original, IEnumerable<Edge> edges)
        {
            var snappedValue = original;
            var alreadySnapped = false;

            var enumerator = edges.GetEnumerator();
            while (enumerator.MoveNext() && !alreadySnapped)
            {
                var currentEdge = enumerator.Current;

                snappedValue = MathOperations.Snap(original, currentEdge.AxisDistance, Threshold);

                alreadySnapped = ValueIsSnapped(original, snappedValue);
            }

            return snappedValue;
        }

        private static bool ValueIsSnapped(double original, double result)
        {
            return Math.Abs(result - original) > 0.1;
        }

        protected override void SourceRectangleFiltered()
        {
            base.SourceRectangleFiltered();

            var horizontalSnapped = HorizontalEdges.Where(EdgeIsSnappedToSnappableHorizontalEdges);
            var verticalSnapped = VerticalEdges.Where(EdgeIsSnappedToSnappableVerticalEdges);

            var snappedEdges = horizontalSnapped.Concat(verticalSnapped);

            UnderlyingSnappedEdges.SynchronizeListTo(snappedEdges.ToList());
        }

        private bool EdgeIsSnappedToSnappableHorizontalEdges(Edge edge)
        {
            const double tolerance = 0.1;
            var snappedToTop = Math.Abs(edge.AxisDistance - Snappable.Top) < tolerance;
            var snappedToBottom = Math.Abs(edge.AxisDistance - Snappable.Bottom) < tolerance;
            return snappedToTop || snappedToBottom;
        }

        private bool EdgeIsSnappedToSnappableVerticalEdges(Edge edge)
        {
            const double tolerance = 0.1;
            var snappedToLeft = Math.Abs(edge.AxisDistance - Snappable.Left) < tolerance;
            var snappedToRight = Math.Abs(edge.AxisDistance - Snappable.Right) < tolerance;
            return snappedToLeft || snappedToRight;
        }
    }
}