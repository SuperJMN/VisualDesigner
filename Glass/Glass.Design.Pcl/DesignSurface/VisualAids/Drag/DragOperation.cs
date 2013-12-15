using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Drag
{
    public class DragOperation
    {
        private ICanvasItem Child { get; set; }
        private Point StartingPoint { get; set; }

        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public DragOperation(ICanvasItem child, Point startingPoint)
        {
            Child = child;

            StartingPoint = startingPoint;
            ChildStartingPoint = child.GetLocation();
        }

        public Point ChildStartingPoint { get; set; }

        public void NotifyNewPosition(Point newPoint)
        {
            var delta = newPoint - StartingPoint;
            var newChildLocation = ChildStartingPoint + delta;

            var resultingLocation = SnappingEngine.SnapPoint(newChildLocation);

            Child.SetLocation(resultingLocation);
        }
    }
}
