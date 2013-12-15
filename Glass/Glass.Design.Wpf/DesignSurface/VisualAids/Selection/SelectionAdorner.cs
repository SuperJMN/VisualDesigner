using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Selection
{
    internal class SelectionAdorner : Adorner
    {
        private static readonly SolidColorBrush FillBrushInstance = new SolidColorBrush(Color.FromArgb(50, 147, 218, 255));
        private static readonly SolidColorBrush PenBrushInstance = new SolidColorBrush(Color.FromArgb(139, 56, 214, 255));
        private static readonly Pen PenInstance = new Pen(PenBrushInstance, 2);
        private ICanvasItem canvasItem;

        private ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                if (canvasItem != null)
                {
                    canvasItem.LeftChanged -= CanvasItemOnPositionChanged;
                    canvasItem.TopChanged -= CanvasItemOnPositionChanged;
                    canvasItem.WidthChanged -= CanvasItemOnSizeChanged;
                    canvasItem.HeightChanged -= CanvasItemOnSizeChanged;
                }
                canvasItem = value;
                if (canvasItem != null)
                {
                    canvasItem.LeftChanged += CanvasItemOnPositionChanged;
                    canvasItem.TopChanged += CanvasItemOnPositionChanged;
                    canvasItem.WidthChanged += CanvasItemOnSizeChanged;
                    canvasItem.HeightChanged += CanvasItemOnSizeChanged;
                }
            }
        }

        private void CanvasItemOnSizeChanged(object sender, SizeChangeEventArgs sizeChangeEventArgs)
        {
            InvalidateVisual();
        }

        private void CanvasItemOnPositionChanged(object sender, LocationChangedEventArgs eventArgs)
        {
            InvalidateVisual();
        }

        private static SolidColorBrush FillBrush
        {
            get { return FillBrushInstance; }
        }

        private static Pen Pen
        {
            get { return PenInstance; }
        }

        public SelectionAdorner(UIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement)
        {        
            CanvasItem = canvasItem;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(canvasItem.Width, canvasItem.Height);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(FillBrush, Pen, new Rect(CanvasItem.Left, CanvasItem.Top, CanvasItem.Width, CanvasItem.Height));
        }
    }
}