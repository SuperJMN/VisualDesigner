using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Glass.Basics.Core;

namespace Glass.Basics.Behaviors.RubberBand
{
    public class RubberBandAdorner : Adorner
    {
        private const double Threshold = 3;
        private readonly Brush brush;
        private Point currentPoint;

        private bool isDragging;
        private Point startPoint;

        public RubberBandAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
            adornedElement.MouseMove += AdornedElementOnMouseMove;
            adornedElement.MouseLeftButtonDown += AdornedElementOnMouseLeftButtonDown;
            adornedElement.PreviewMouseLeftButtonUp += AdornedElementOnPreviewMouseLeftButtonUp;
            brush = new SolidColorBrush(SystemColors.HighlightColor) { Opacity = 0.3 };
        }

        private void AdornedElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (!IsEnabled)
            {
                return;
            }

            startPoint = Mouse.GetPosition(AdornedElement);
            currentPoint = startPoint;

            Mouse.Capture(AdornedElement);
            OnDragStarted();
            isDragging = true;
        }

        private void AdornedElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.LeftButton == MouseButtonState.Pressed && isDragging)
            {
                currentPoint = Mouse.GetPosition(AdornedElement);
                InvalidateVisual();

                var rect = new Rect(startPoint, currentPoint);

                OnDragMove(new EventArgs<Rect>(rect));
            }
        }


        private void AdornedElementOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (!IsEnabled || !isDragging)
            {
                return;
            }

            var rect = new Rect(startPoint, currentPoint);

            isDragging = false;
            DisposeRubberBand();
            OnDragFinished(new EventArgs<Rect>(rect));
        }

        private static bool SurpassesThreshold(Rect rect)
        {
            return rect.Size.Height > Threshold && rect.Size.Width > Threshold;
        }

        private void DisposeRubberBand()
        {
            AdornedElement.ReleaseMouseCapture();
            InvalidateVisual();
        }

        private void OnDragMove(EventArgs<Rect> e)
        {
            if (DragMove != null)
                DragMove(this, e);
        }

        private void OnDragStarted()
        {
            if (DragStarted != null)
                DragStarted(this, EventArgs.Empty);
        }

        private void OnDragFinished(EventArgs<Rect> e)
        {
            if (DragFinished != null)
                DragFinished(this, e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var rect = new Rect(startPoint, currentPoint);
            if (SurpassesThreshold(rect) && isDragging)
            {
                const double thickness = 1.0;
                const double thicknessOffset = thickness / 2;

                drawingContext.PushGuidelineSet(new GuidelineSet(new[] { startPoint.X - thicknessOffset, currentPoint.X - thicknessOffset }, new[] { startPoint.Y - thicknessOffset, currentPoint.Y - thicknessOffset }));
                drawingContext.DrawGeometry(brush, new Pen(SystemColors.HighlightBrush, thickness), new RectangleGeometry(rect));
            }

            base.OnRender(drawingContext);
        }

        public event EventHandler<EventArgs<Rect>> DragFinished;
        public event EventHandler<EventArgs<Rect>> DragMove;
        public event EventHandler DragStarted;        
    }
}