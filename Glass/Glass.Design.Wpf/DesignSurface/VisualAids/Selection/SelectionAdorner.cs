using System.Windows;
using System.Windows.Media;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;
using Rect = System.Windows.Rect;
using Size = System.Windows.Size;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Selection
{
    internal class SelectionAdorner : CanvasItemAdorner
    {
        private static readonly SolidColorBrush FillBrushInstance = new SolidColorBrush(Color.FromArgb(50, 147, 218, 255));
        private static readonly SolidColorBrush PenBrushInstance = new SolidColorBrush(Color.FromArgb(139, 56, 214, 255));
        private static readonly Pen PenInstance = new Pen(PenBrushInstance, 2);
       

 


        private static SolidColorBrush FillBrush
        {
            get { return FillBrushInstance; }
        }

        private static Pen Pen
        {
            get { return PenInstance; }
        }

        public SelectionAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base((UIElement) adornedElement.GetCoreInstance(), canvasItem)
        {        
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(CanvasItem.Width, CanvasItem.Height);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(FillBrush, Pen, new Rect(CanvasItem.Left, CanvasItem.Top, CanvasItem.Width, CanvasItem.Height));
        }
    }
}