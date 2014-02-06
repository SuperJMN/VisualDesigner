using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
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
                SnappingEngine.Snappable = child;
            }
        }

        private IPoint StartingPoint { get; set; }

        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public DragOperation(ICanvasItem child, IPoint startingPoint, ISnappingEngine snappingEngine)
        {
            SnappingEngine = snappingEngine;
            Child = child;                                   
            StartingPoint = startingPoint;            
            ChildStartingPoint = child.GetLocation();
        }

        public IPoint ChildStartingPoint { get; set; }

        public void NotifyNewPosition(IPoint newPoint)
        {
            var delta = newPoint.Subtract(StartingPoint);
            var newChildLocation = ChildStartingPoint.Add(delta);

            var originalRect = ServiceLocator.CoreTypesFactory.CreateRect(newChildLocation.X, newChildLocation.Y, Child.Width, Child.Height);

            SnappingEngine.SetSourceRectForDrag(originalRect);                                                
        }
    }
}

