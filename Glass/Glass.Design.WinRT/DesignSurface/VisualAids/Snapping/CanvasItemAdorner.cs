using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Snapping
{
    public abstract class CanvasItemAdorner : Control, IAdorner
    {
        private ICanvasItem canvasItem;

        protected CanvasItemAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
        {
            AdornedElement = adornedElement;
            CanvasItem = canvasItem;
        }

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

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        public double GetCoordinate(CoordinatePart part)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new System.NotImplementedException();
        }

        public double Left
        {
            get { return CanvasItem.Left; }
            set
            {
                CanvasItem.Left = value;      
                OnPropertyChanged(new PropertyChangedEventArgs("Left"));
            }
        }

        public double Top
        {
            get { return CanvasItem.Top; }
            set
            {
                CanvasItem.Top = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Top"));
            }
        }

        public double Width
        {
            get { return CanvasItem.Width; }
            set
            {
                CanvasItem.Width = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Width"));
            }
        }

        public double Height
        {
            get { return CanvasItem.Height; }
            set
            {
                CanvasItem.Height = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Height"));
            }
        }

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
        
        public abstract object GetCoreInstance();
        

        public IUIElement AdornedElement { get; set; }

        public ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                canvasItem = value;
                canvasItem.PropertyChanged += CanvasItemOnPropertyChanged;
            }
        }

        private void CanvasItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            // Whenever a property in the CanvasItem changes, the properties of the adorner should change, too.
            this.Left = CanvasItem.Left;
            this.Top = CanvasItem.Top;
        }
    }
}