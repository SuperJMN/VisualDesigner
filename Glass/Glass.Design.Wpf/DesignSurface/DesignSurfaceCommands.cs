using System.Windows.Input;

namespace Glass.Design.Wpf.DesignSurface
{
    public static class DesignSurfaceCommands
    {
        static readonly RoutedUICommand GroupCommandInstance = new RoutedUICommand("Group", "Group", typeof(DesignSurface));
        static readonly RoutedUICommand PromoteChildrenCommandInstance = new RoutedUICommand("Ungroup", "Ungroup", typeof(DesignSurface));
        
        private static readonly RoutedUICommand AlignHorizontallyLeftCommandInstance = new RoutedUICommand("Align to left", "AlignToLeft", typeof(DesignSurface));
        private static readonly RoutedUICommand AlignHorizontallyCenterCommandInstance = new RoutedUICommand("Align to center", "AlignToCenter", typeof(DesignSurface));
        private static readonly RoutedUICommand AlignHorizontallyRightCommandInstance = new RoutedUICommand("Align to right", "AlignToRight", typeof(DesignSurface));

        private static readonly RoutedUICommand AlignVerticallyTopCommandInstance = new RoutedUICommand("Align to top", "AlignToTop", typeof(DesignSurface));
        private static readonly RoutedUICommand AlignVerticallyCenterCommandInstance = new RoutedUICommand("Align to middle", "AlignToCenter", typeof(DesignSurface));
        private static readonly RoutedUICommand AlignVerticallyBottomCommandInstance = new RoutedUICommand("Align to right", "AlignToBottom", typeof(DesignSurface));

        private static readonly RoutedUICommand BringToFrontCommandInstance = new RoutedUICommand("Bring to front", "BringToFront", typeof(DesignSurface));
        private static readonly RoutedUICommand SendToBackCommandInstance = new RoutedUICommand("Send to back", "SendToBack", typeof(DesignSurface));

        public static RoutedUICommand GroupCommand
        {
            get { return GroupCommandInstance; }
        }

        public static RoutedUICommand PromoteChildrenCommand
        {
            get { return PromoteChildrenCommandInstance; }
        }

        public static RoutedUICommand AlignHorizontallyLeftCommand
        {
            get { return AlignHorizontallyLeftCommandInstance; }
        }

        public static RoutedUICommand AlignHorizontallyCenterCommand
        {
            get { return AlignHorizontallyCenterCommandInstance; }
        }

        public static RoutedUICommand AlignHorizontallyRightCommand
        {
            get { return AlignHorizontallyRightCommandInstance; }
        }

        public static RoutedUICommand AlignVerticallyTopCommand
        {
            get { return AlignVerticallyTopCommandInstance; }
        }

        public static RoutedUICommand AlignVerticallyCenterCommand
        {
            get { return AlignVerticallyCenterCommandInstance; }
        }

        public static RoutedUICommand AlignVerticallyBottomCommand
        {
            get { return AlignVerticallyBottomCommandInstance; }
        }

        public static RoutedUICommand BringToFrontCommand
        {
            get { return BringToFrontCommandInstance; }
        }

        public static RoutedUICommand SendToBackCommand
        {
            get { return SendToBackCommandInstance; }
        }
    }
}