using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Glass.Basics.Presentation.Rubberband
{
    public class RubberBandAttacher
    {
        private readonly UIElement element;
        private readonly AdornerLayer adornerLayer;
        private RubberBandAdorner adorner;
        private Point mouseDownPoint;

        private readonly Brush stroke = SystemColors.HighlightBrush;
        private readonly DoubleCollection strokeDashArray = new DoubleCollection(new[] { 3D, 3D });
        private readonly Brush fill;

        public RubberBandAttacher(UIElement element)
        {
            this.element = element;
            adornerLayer = AdornerLayer.GetAdornerLayer(element);

            var highlightColor = SystemColors.HighlightBrush.Color;
            fill = new SolidColorBrush(Color.FromArgb(32, highlightColor.R, highlightColor.G, highlightColor.B));

            element.MouseLeftButtonDown += ElementOnMouseLeftButtonDown;
        }

        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            mouseDownPoint = mouseButtonEventArgs.GetPosition(element);
            adorner = new RubberBandAdorner(element, new Rectangle { Stroke = stroke, StrokeDashArray = strokeDashArray, Fill = fill });
            element.MouseMove += ElementOnMouseMove;
            element.MouseLeftButtonUp += ElementOnMouseLeftButtonUp;
            element.CaptureMouse();
            adornerLayer.Add(adorner);
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {

            var currentPoint = mouseButtonEventArgs.GetPosition(element);

            var rect = new Rect(mouseDownPoint, currentPoint);

            element.ReleaseMouseCapture();
            element.MouseLeftButtonUp -= ElementOnMouseLeftButtonUp;
            element.MouseMove -= ElementOnMouseMove;
            adornerLayer.Remove(adorner);
            OnDragCompleted(rect);
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var currentPoint = mouseEventArgs.GetPosition(element);

            var rect = new Rect(mouseDownPoint, currentPoint);
            adorner.Left = rect.Left;
            adorner.Top = rect.Top;
            adorner.Width = rect.Width;
            adorner.Height = rect.Height;
        }

        public event RectEventHandler DragCompleted;

        protected virtual void OnDragCompleted(Rect rect)
        {
            var handler = DragCompleted;
            if (handler != null)
            {
                handler(this, rect);
            }
        }
    }
}