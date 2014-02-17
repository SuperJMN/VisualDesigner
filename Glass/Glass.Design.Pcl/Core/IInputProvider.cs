using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.Core
{
    public interface IInputProvider
    {
        Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver); 
    }
}