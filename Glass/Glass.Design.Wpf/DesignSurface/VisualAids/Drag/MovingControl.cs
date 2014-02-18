using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Drag
{
    public class MovingControl : Control, IUIElement
    {       
        static MovingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovingControl), new FrameworkPropertyMetadata(typeof(MovingControl)));
        }

        public event FingerManipulationEventHandler FingerDown;

        protected virtual void OnFingerDown(FingerManipulationEventArgs args)
        {
            var handler = FingerDown;
            if (handler != null) handler(this, args);
        }

        public event FingerManipulationEventHandler FingerMove;

        protected virtual void OnFingerMove(FingerManipulationEventArgs args)
        {
            var handler = FingerMove;
            if (handler != null) handler(this, args);
        }

        public event FingerManipulationEventHandler FingerUp;

        protected virtual void OnFingerUp(FingerManipulationEventArgs args)
        {
            var handler = FingerUp;
            if (handler != null) handler(this, args);
        }

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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            OnFingerDown(new FingerManipulationEventArgs());
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            OnFingerUp(new FingerManipulationEventArgs());
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            OnFingerMove(new FingerManipulationEventArgs());
        }


        public bool IsVisible { get; set; }
        public object GetCoreInstance()
        {
            return this;
        }
    }
}