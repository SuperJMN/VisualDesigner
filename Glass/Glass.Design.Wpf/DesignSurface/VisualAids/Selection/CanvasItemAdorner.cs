using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

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
        public void CaptureInput(object pointer)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInput(object pointer)
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
}