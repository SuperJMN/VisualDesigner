using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.Core
{
    public static class ServiceLocator
    {
        public static ICoreTypesFactory CoreTypesFactory { get; set; }
        public static IInputProvider InputProvider { get; set; }
    }

    public interface IInputProvider
    {
        Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver); 
    }
}