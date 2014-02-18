using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Drag
{
    public class MovingControl : Control, IControl
    {
        public MovingControl()
        {
            DefaultStyleKey = typeof (MovingControl);
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
        public CanvasItemCollection Children { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public ICanvasItemContainer Parent { get; private set; }
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
            throw new System.NotImplementedException();
        }
    }
}