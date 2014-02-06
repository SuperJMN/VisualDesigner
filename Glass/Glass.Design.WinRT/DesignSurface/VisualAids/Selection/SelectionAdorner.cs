using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
    public class CanvasItemAdorner : UIElement
    {
        private ICanvasItem canvasItem;

        public CanvasItemAdorner(UIElement adornedElement, ICanvasItem canvasItem)
         //   : base(adornedElement)
        {        
            CanvasItem = canvasItem;
        }

        protected ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                if (canvasItem != null)
                {
                    canvasItem.PropertyChanged -= CanvasItemOnPropertyChanged;
                }
                canvasItem = value;
                if (canvasItem != null)
                {
                    canvasItem.PropertyChanged += CanvasItemOnPropertyChanged;
                }
            }
        }

        private void CanvasItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Top":
                case "Left":
                case "Width":
                case "Height":
                    InvalidateVisual();
                    break;
            }
        }

        private void InvalidateVisual()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Adorner : UIElement
    {
    }

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

        public SelectionAdorner(UIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {        
        }

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    return new Size(CanvasItem.Width, CanvasItem.Height);
        //}

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);

        //    drawingContext.DrawRectangle(FillBrush, Pen, new Rect(CanvasItem.Left, CanvasItem.Top, CanvasItem.Width, CanvasItem.Height));
        //}
    }

    internal class Pen
    {
        public Pen(SolidColorBrush penBrushInstance, int i)
        {
            throw new System.NotImplementedException();
        }
    }
}