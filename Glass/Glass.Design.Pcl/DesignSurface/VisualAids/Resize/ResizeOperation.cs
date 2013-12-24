using System;
using System.Dynamic;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class ResizeOperation
    {
        private ICanvasItem child;
        private ProportionalResizer Resizer { get; set; }


        private ICanvasItem Child
        {
            get { return child; }
            set
            {
                child = value;
                SnappingEngine.Snappable = child;
            }
        }

        private IPoint InitialRelativeHandlePosition { get; set; }

        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public ResizeOperation(ICanvasItem child, IPoint handleRelativeToParent, ISnappingEngine snappingEngine)
        {
            SnappingEngine = snappingEngine;

            Child = child;

            UpdateCoordinates(handleRelativeToParent);



            Resizer = new ProportionalResizer(Child) { HookPoint = HookPoint };
        }

        private void UpdateCoordinates(IPoint startingHandlePosition)
        {
            

            var local = startingHandlePosition.FromParentToLocal(Child.GetLocation());

            var bound = ServiceLocator.CoreTypesFactory.CreateRect(0, 0, Child.Width, Child.Height);

            var middlePoint = bound.GetMiddlePoint();
            var opposite = local.GetOpposite(middlePoint);

            InitialRelativeHandlePosition = startingHandlePosition;
            HookPoint = ServiceLocator.CoreTypesFactory.CreatePoint(opposite.X/Child.Width, opposite.Y/Child.Height);
        }

        private IPoint HookPoint { get; set; }        


        public void UpdateHandlePosition(IPoint newHandlePosition)
        {
            var delta = newHandlePosition.Subtract(InitialRelativeHandlePosition);
            var point = ServiceLocator.CoreTypesFactory.CreateVector(delta.X, delta.Y);

            Resizer.DeltaResize(point);


            //var originalRect = ServiceLocator.CoreTypesFactory.CreateRect(newChildLocation.X, newChildLocation.Y, Child.Width, Child.Height);

            //SnappingEngine.SetSourceRect(originalRect);
        }
    }
}