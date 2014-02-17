using System.Windows.Controls;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Wpf.DesignSurface
{
    public abstract class DesignSurfaceBase : ListBox, IUserInputReceiver
    {
        public event FingerManipulationEventHandler FingerDown;

        public event FingerManipulationEventHandler FingerMove;

        public event FingerManipulationEventHandler FingerUp;

       
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

        public void CaptureInput()
        {
            //CapturePointer(new Pointer());
        }

        public void ReleaseInput()
        {
            //ReleasePointerCaptures();
        }
    }
}