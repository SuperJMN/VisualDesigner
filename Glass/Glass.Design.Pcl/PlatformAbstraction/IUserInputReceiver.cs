namespace Glass.Design.Pcl.PlatformAbstraction
{
    public interface IUserInputReceiver
    {
        event FingerManipulationEventHandler FingerDown;
        event FingerManipulationEventHandler FingerMove;
        event FingerManipulationEventHandler FingerUp;
        void CaptureInput();
        void ReleaseInput();
    }
}