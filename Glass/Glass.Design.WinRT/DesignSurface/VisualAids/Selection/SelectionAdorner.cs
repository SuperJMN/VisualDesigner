using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
    public class SelectionAdorner : CanvasItemAdorner
    {
        public SelectionAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {

        }
        
        public override object GetCoreInstance()
        {
            var rectangle = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Red),
                Opacity = 0.5,
                Width = CanvasItem.Width,
                Height = CanvasItem.Height,
            };

            return rectangle;
        }      
    }

    public abstract class Adorner : IAdorner
    {
        public Adorner(IUIElement adornedElement)
        {
            AdornedElement = adornedElement;
        }

        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
        public void CaptureInput()
        {
            throw new NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public CanvasItemCollection Children { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public ICanvasItemContainer Parent { get; private set; }
        public void AddAdorner(IAdorner adorner)
        {
            throw new NotImplementedException();
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            throw new NotImplementedException();
        }

        public bool IsVisible { get; set; }
        public bool IsHitTestVisible { get; set; }
        public abstract object GetCoreInstance();
        

        public IUIElement AdornedElement { get; set; }
    }

    public abstract class CanvasItemAdorner : Adorner
    {
        private ICanvasItem canvasItem;

        public CanvasItemAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement)
        {
            CanvasItem = canvasItem;
        }

        protected ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                canvasItem = value;
                this.Left = canvasItem.Left;
                this.Top = canvasItem.Top;
                this.Width = canvasItem.Width;
                this.Height = canvasItem.Height;
            }
        }
    }
}