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

        public ResizeOperation(ICanvasItem child, IPoint handlePosition, ISnappingEngine snappingEngine)
        {
            SnappingEngine = snappingEngine;

            Child = child;

            InitialRelativeHandlePosition = GetInitialPosition(handlePosition);
            Anchor = GetAnchor(handlePosition);
            InitialChildSize = Child.GetSize();


            Resizer = new ProportionalResizer(Child) { Anchor = Anchor };
        }

        private ISize InitialChildSize { get; set; }

        private static IPoint GetAnchor(IPoint handlePosition)
        {
            var middlePoint = ServiceLocator.CoreTypesFactory.CreatePoint(0.5, 0.5);
            return handlePosition.GetOpposite(middlePoint);
        }

        private IPoint GetInitialPosition(IPoint handlePosition)
        {
            var xAbsoluteToChild = Child.Width * handlePosition.X;
            var yAbsoluteToChild = Child.Height * handlePosition.Y;

            var pointAbsoluteToChild = ServiceLocator.CoreTypesFactory.CreatePoint(xAbsoluteToChild, yAbsoluteToChild);
            var fromLocalToParent = pointAbsoluteToChild.FromLocalToParent(Child.GetLocation());
            return fromLocalToParent;
        }

        private IPoint Anchor { get; set; }


        public void UpdateHandlePosition(IPoint newHandlePosition)
        {
            var delta = newHandlePosition.Subtract(InitialRelativeHandlePosition).ToVector();            

            if (Anchor.X > 0.5)
            {
                delta.X = -delta.X;
            }
            if (Anchor.Y > 0.5)
            {
                delta.Y = -delta.Y;
            }

            var childWidth = InitialChildSize.ToVector().Add(delta);
            Resizer.Resize(childWidth.ToSize());


            //var originalRect = ServiceLocator.CoreTypesFactory.CreateRect(newChildLocation.X, newChildLocation.Y, Child.Width, Child.Height);

            //SnappingEngine.SetSourceRect(originalRect);
        }
    }
}