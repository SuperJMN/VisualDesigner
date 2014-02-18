namespace Glass.Design.Pcl.PlatformAbstraction
{
    public interface IUserInputReceiver
    {
        event FingerManipulationEventHandler FingerDown;
        event FingerManipulationEventHandler FingerMove;
        event FingerManipulationEventHandler FingerUp;
        void CaptureInput(object pointer);
        void ReleaseInput(object pointer);
    }
}