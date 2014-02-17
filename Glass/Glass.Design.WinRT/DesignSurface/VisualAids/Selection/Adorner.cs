using System;
using System.ComponentModel;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
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
}