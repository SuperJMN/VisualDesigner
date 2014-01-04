using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.Annotations;
using Glass.Design.Wpf.Core;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Snapping
{
    public class EdgeAdorner : Adorner
    {
        static EdgeAdorner()
        {
            var color = Color.FromArgb(128, 255, 0, 0);
            Pen = new Pen(new SolidColorBrush(color), 2);
            Pen.DashStyle = new DashStyle(new[] { 2D, 2D }, 0);
        }

        private static Pen Pen { get; set; }

        private CanvasItem item;

        public CanvasItem Item
        {
            get { return item; }
            set
            {
                if (item != null)
                {
                    item.LeftChanged -= LocationChanged;
                    item.TopChanged -= LocationChanged;
                    item.WidthChanged -= ItemOnWidthChanged;
                    item.HeightChanged -= ItemOnWidthChanged;
                }

                item = value;

                if (item != null)
                {
                    item.LeftChanged += LocationChanged;
                    item.TopChanged += LocationChanged;
                    item.WidthChanged += ItemOnWidthChanged;
                    item.HeightChanged += ItemOnWidthChanged;
                }
            }
        }

        private void ItemOnWidthChanged(object sender, SizeChangeEventArgs sizeChangeEventArgs)
        {
            InvalidateVisual();
        }

        private void LocationChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {
            InvalidateVisual();
        }

        public Edge Edge { get; set; }

        public EdgeAdorner([NotNull] UIElement adornedElement, CanvasItem item, Edge edge)
            : base(adornedElement)
        {
            Item = item;
            Edge = edge;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
           

            double segmentStart;
            double segmentEnd;
            if (Edge.Orientation == Orientation.Vertical)
            {
                segmentStart = Math.Min(Edge.Range.SegmentStart, Item.Top);
                segmentEnd = Math.Max(Edge.Range.SegmentEnd, Item.Bottom);
            }
            else
            {
                segmentStart = Math.Min(Edge.Range.SegmentStart, Item.Left);
                segmentEnd = Math.Max(Edge.Range.SegmentEnd, Item.Right);
            }

            var point1 = new Point(Edge.AxisDistance, segmentStart);
            var point2 = new Point(Edge.AxisDistance, segmentEnd);
            
            if (Edge.Orientation == Orientation.Horizontal)
            {
                point1 = point1.Swap();
                point2 = point2.Swap();
            }

            drawingContext.DrawLine(Pen, point1, point2);
        }
    }
}