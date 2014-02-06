using Windows.UI;
using Windows.UI.Xaml;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.WinRT.DesignSurface.VisualAids.Selection;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Snapping
{
    public class EdgeAdorner : CanvasItemAdorner
    {
        static EdgeAdorner()
        {
            var color = Color.FromArgb(128, 255, 0, 0);
            //Pen = new Pen(new SolidColorBrush(color), 2);
            //Pen.DashStyle = new DashStyle(new[] { 2D, 2D }, 0);
        }

        public EdgeAdorner(UIElement adornedElement, ICanvasItem canvasItem) : base(adornedElement, canvasItem)
        {
        }

        private static Pen Pen { get; set; }

        
        public Edge Edge { get; set; }

        //public EdgeAdorner([NotNull] UIElement adornedElement, CanvasItem item, Edge edge)
        //    : base(adornedElement, item)
        //{
        //    Edge = edge;
        //}

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    //base.OnRender(drawingContext);
           

        //    double segmentStart;
        //    double segmentEnd;
        //    if (Edge.Orientation == Orientation.Vertical)
        //    {
        //        segmentStart = Math.Min(Edge.Range.SegmentStart, CanvasItem.Top);
        //        segmentEnd = Math.Max(Edge.Range.SegmentEnd, CanvasItem.Bottom);
        //    }
        //    else
        //    {
        //        segmentStart = Math.Min(Edge.Range.SegmentStart, CanvasItem.Left);
        //        segmentEnd = Math.Max(Edge.Range.SegmentEnd, CanvasItem.Right);
        //    }

        //    var point1 = new Point(Edge.AxisDistance, segmentStart);
        //    var point2 = new Point(Edge.AxisDistance, segmentEnd);
            
        //    if (Edge.Orientation == Orientation.Horizontal)
        //    {
        //        point1 = point1.Swap();
        //        point2 = point2.Swap();
        //    }

        //    drawingContext.DrawLine(Pen, point1, point2);
        //}
    }
}