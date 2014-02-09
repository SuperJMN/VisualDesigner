using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;
using Rect = System.Windows.Rect;
using Size = System.Windows.Size;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Selection
{
    public class CanvasItemAdorner : Adorner, IAdorner
    {
        private ICanvasItem canvasItem;

        public CanvasItemAdorner(UIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement)
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

        public new IUIElement AdornedElement { get; set; }
        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
        public void CaptureInput()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new System.NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new System.NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public CanvasItemCollection Children { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public ICanvasItemContainer Parent { get; set; }
        public void AddAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }

        public bool IsVisible { get; set; }
        public object GetCoreInstance()
        {
            return this;
        }
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