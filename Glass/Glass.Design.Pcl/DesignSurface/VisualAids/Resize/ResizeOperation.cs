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

        private ICanvasItem Child
        {
            get { return child; }
            set
            {
                child = value;
            }
        }

        public IPoint HandlePoint { get; set; }



        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public ResizeOperation(ICanvasItem child, IPoint handlePoint, ISnappingEngine snappingEngine)
        {
            Child = child;
            HandlePoint = ConvertToAbsolute(handlePoint, Child);
            Opposite = HandlePoint.GetOpposite(child.Rect().MiddlePoint());
            SnappingEngine = snappingEngine;        
        }

        private IPoint ConvertToAbsolute(IPoint handlePoint, ICanvasItem child)
        {
            var x = 0D;
            var y = 0D;
            if (handlePoint.X == 0)
            {
                x = child.Left;
            }
            else
            {
                x = child.Rect().Right;
            }
          
            if (handlePoint.Y == 0)
            {
                y = child.Top;
            }
            else
            {
                y = child.Rect().Bottom;
            }

            var finalPoint = ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
            return finalPoint;
        }

        public IPoint Opposite { get; set; }



        public void UpdateHandlePosition(IPoint newPoint)
        {
            var rect = Child.Rect();

            if (InsideLimits(newPoint.X))
            {
                var left = Math.Min(newPoint.X, Opposite.X);
                var right = Math.Max(newPoint.X, Opposite.X);
                rect.X = left;
                rect.SetRightKeepingLeft(right);
            }

            if (rect.Width > 0 && rect.Height > 0)
            {
                Child.SetBounds(rect);    
            }
            
        }

        private bool InsideLimits(double newPoint)
        {
            var firstMember = HandlePoint.X - Opposite.X;
            var secondMember = newPoint - Opposite.X;

            var firstPositive = firstMember > 0;
            var secondPositive = secondMember > 0;

            

            return firstPositive == secondPositive;
        }
    }

    public struct ResizeHandle
    {
        public VerticalAlignment VerticalAlignment { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom,
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right,
    }
}
