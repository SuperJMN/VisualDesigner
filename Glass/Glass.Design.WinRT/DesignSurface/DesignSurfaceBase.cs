using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;
using FoundationPoint = Windows.Foundation.Point;

namespace Glass.Design.WinRT.DesignSurface
{
    public abstract class DesignSurfaceBase : GridView, IUserInputReceiver
    {
        public event FingerManipulationEventHandler FingerDown;

        public event FingerManipulationEventHandler FingerMove;

        public event FingerManipulationEventHandler FingerUp;

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            var currentPoint = e.GetCurrentPoint(this);

            if (currentPoint.Properties.IsLeftButtonPressed)
            {
                var point = new Point(currentPoint.Position.X, currentPoint.Position.Y);
                var args = new FingerManipulationEventArgs
                           {
                               Point = point, Handled = true, Pointer = e.Pointer,
                           };

                OnFingerDown(args);
            }
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            base.OnPointerPressed(e);
            var currentPoint = e.GetCurrentPoint(this);
            var point = new Point(currentPoint.Position.X, currentPoint.Position.Y);

            var args = new FingerManipulationEventArgs { Point = point, Handled = true };

            OnFingerMove(args);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            base.OnPointerPressed(e);
            var currentPoint = e.GetCurrentPoint(this);
            var point = new Point(currentPoint.Position.X, currentPoint.Position.Y);

            var args = new FingerManipulationEventArgs { Point = point, Handled = true };

            OnFingerUp(args);
        }

        protected virtual void OnFingerDown(FingerManipulationEventArgs args)
        {
            var handler = FingerDown;
            if (handler != null) handler(this, args);
        }

        protected virtual void OnFingerMove(FingerManipulationEventArgs args)
        {
            var handler = FingerMove;
            if (handler != null) handler(this, args);
        }

        protected virtual void OnFingerUp(FingerManipulationEventArgs args)
        {
            var handler = FingerUp;
            if (handler != null) handler(this, args);
        }

        public void CaptureInput(object pointer)
        {
            var p = (Pointer) pointer;
            CapturePointer(p);
        }

        public void ReleaseInput(object pointer)
        {            
            ReleasePointerCaptures();
        }
    }
}