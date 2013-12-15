using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Drag
{
    public class DragOperation
    {
        private ICanvasItem Child { get; set; }
        private IPoint StartingPoint { get; set; }

        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public DragOperation(ICanvasItem child, IPoint startingPoint)
        {
            Child = child;

            StartingPoint = startingPoint;
            ChildStartingPoint = child.GetLocation();
        }

        public IPoint ChildStartingPoint { get; set; }

        public void NotifyNewPosition(IPoint newPoint)
        {
            var delta = newPoint.Subtract(StartingPoint);
            var newChildLocation = ChildStartingPoint.Add(delta);

            var resultingLocation = SnappingEngine.SnapPoint(newChildLocation);

            Child.SetLocation(resultingLocation);
        }
    }
}
