using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.PlatformAbstraction
{
    public class FingerManipulationEventArgs
    {
        public Point Point { get; set; }
        public bool Handled { get; set; }

        public object Pointer { get; set; }

        public Point GetPosition(IUserInputReceiver relativeTo)
        {
            return ServiceLocator.InputProvider.GetMousePositionRelativeTo(relativeTo);
        }
    }
}