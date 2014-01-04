using System;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class ResizeOperation
    {
        private ICanvasItem Child { get; set; }

        private IPoint HandlePoint { get; set; }

        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public ResizeOperation(ICanvasItem child, IPoint handlePoint, ISnappingEngine snappingEngine)
        {
            Child = child;
            HandlePoint = handlePoint;
            SetCanResize(child, handlePoint);
            Opposite = HandlePoint.GetOpposite(child.Rect().MiddlePoint());
            SnappingEngine = snappingEngine;        
        }

        private void SetCanResize(ICanvasItem child, IPoint handlePoint)
        {
            var rect = child.Rect();
            var middlePoint = rect.MiddlePoint();

            var distanceX = Math.Abs(middlePoint.X - handlePoint.X);
            var distanceY = Math.Abs(middlePoint.Y - handlePoint.Y);

            double delta = 0.3;
            CanChangeHorizontalPosition = distanceX > delta*child.Width;
            CanChangeVerticalPosition = distanceY > delta * child.Height;
        }

        public IPoint Opposite { get; set; }



        public void UpdateHandlePosition(IPoint newPoint)
        {
            var rect = Child.Rect();

            if (InsideLimitsX(newPoint.X) && CanChangeHorizontalPosition)
            {
                var left = Math.Min(newPoint.X, Opposite.X);
                var right = Math.Max(newPoint.X, Opposite.X);
                rect.X = left;
                rect.SetRightKeepingLeft(right);
            }

            if (InsideLimitsY(newPoint.Y) && CanChangeVerticalPosition)
            {
                var top = Math.Min(newPoint.Y, Opposite.Y);
                var bottom = Math.Max(newPoint.Y, Opposite.Y);
                rect.Y = top;
                rect.SetBottomKeepingTop(bottom);
            }

            if (rect.Width > 0 && rect.Height > 0)
            {
                SnappingEngine.SetSourceRectForResize(rect);
                
                //Child.SetBounds(rect);    
            }
            
        }

        public bool CanChangeVerticalPosition { get; set; }

        private bool CanChangeHorizontalPosition { get; set; }

        private bool InsideLimitsX(double newPoint)
        {
            var firstMember = HandlePoint.X - Opposite.X;
            var secondMember = newPoint - Opposite.X;

            var firstPositive = firstMember > 0;
            var secondPositive = secondMember > 0;

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

            return firstPositive == secondPositive;
        }

        private bool InsideLimitsY(double newPoint)
        {
            var firstMember = HandlePoint.Y - Opposite.Y;
            var secondMember = newPoint - Opposite.Y;

            var firstPositive = firstMember > 0;
            var secondPositive = secondMember > 0;



            return firstPositive == secondPositive;
        }
    }   
}
