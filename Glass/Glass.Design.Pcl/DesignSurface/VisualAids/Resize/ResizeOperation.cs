using System;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using PostSharp.Patterns.Recording;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class ResizeOperation : IDisposable
    {
        private ICanvasItem child;
        private ISnappingEngine snappingEngine;
        private RecordingScope recordingScope;

        private ICanvasItem Child
        {
            get { return child; }
            set
            {
                child = value;

                if (SnappingEngine != null)
                {
                    SnappingEngine.Snappable = value;
                }
            }
        }

        private IPoint HandlePoint { get; set; }

        [NotNull]
        private ISnappingEngine SnappingEngine
        {
            get { return snappingEngine; }
            set
            {
                snappingEngine = value;
                if (Child != null)
                {
                    SnappingEngine.Snappable = Child;
                }
            }
        }

        public ResizeOperation(ICanvasItem child, IPoint handlePoint, ISnappingEngine snappingEngine)
        {
            Child = child;
            HandlePoint = handlePoint;
            SetCanResize(child, handlePoint);
            Opposite = HandlePoint.GetOpposite(child.Rect().MiddlePoint());
            SnappingEngine = snappingEngine;
            this.recordingScope = RecordingServices.DefaultRecorder.OpenScope(string.Format( "Resize {0}", this.child.GetName() ));
        }

        private void SetCanResize(ICanvasItem canvasItem, IPoint handlePoint)
        {
            var rect = canvasItem.Rect();
            var middlePoint = rect.MiddlePoint();

            var distanceX = Math.Abs(middlePoint.X - handlePoint.X);
            var distanceY = Math.Abs(middlePoint.Y - handlePoint.Y);

            double delta = 0.3;
            CanChangeHorizontalPosition = distanceX > delta*canvasItem.Width;
            CanChangeVerticalPosition = distanceY > delta * canvasItem.Height;
        }

        private IPoint Opposite { get; set; }

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
            }
            
        }

        private bool CanChangeVerticalPosition { get; set; }

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
            var signOfLimit = Math.Sign(HandlePoint.Y - Opposite.Y);
            var signOfNewPoint = Math.Sign(newPoint - Opposite.Y);

            return signOfLimit == signOfNewPoint;
        }

        public void Dispose()
        {
            this.recordingScope.Dispose();
        }
    }   
}
