using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Drag
{
    public class DragOperation
    {
        private ICanvasItem child;

        private ICanvasItem Child
        {
            get { return child; }
            set
            {
                child = value;
                var fakeCanvasItem = new FakeCanvasItem();
                fakeCanvasItem.SetLocation(child.GetLocation());
                fakeCanvasItem.SetSize(child.GetSize());
                FakeCanvasItem = fakeCanvasItem;
            }
        }

        public FakeCanvasItem FakeCanvasItem { get; set; }

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

            var snappedX = SnappingEngine.SnapPoint(newChildLocation.X);
            var snappedY = SnappingEngine.SnapPoint(newChildLocation.Y);

            Child.Left = snappedX;
            Child.Top = snappedY;
        }
    }
}

