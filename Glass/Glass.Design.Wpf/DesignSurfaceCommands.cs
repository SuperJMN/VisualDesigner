using System.Windows.Input;

namespace Glass.Design.Wpf
{
    public static class DesignSurfaceCommands
    {
        static readonly ICommand GroupCommandInstance = new RoutedUICommand("Group", "Group", typeof(DesignSurface.DesignSurface));

        public static ICommand GroupCommand
        {
            get { return GroupCommandInstance; }
        }
    }
}