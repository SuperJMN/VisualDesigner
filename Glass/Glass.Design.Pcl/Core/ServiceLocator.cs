using Glass.Design.Pcl.Annotations;

namespace Glass.Design.Pcl.Core
{
    public static class ServiceLocator
    {
        public static ICoreTypesFactory CoreTypesFactory { get; set; }
        public static IInputProvider InputProvider { get; set; }
        public static IUIElementFactory UIElementFactory { get; set; }
    }
}