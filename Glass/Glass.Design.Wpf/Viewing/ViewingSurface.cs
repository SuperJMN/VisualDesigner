using System.Windows;
using System.Windows.Controls;

namespace Glass.Design.Wpf.Viewing
{
    public class ViewingSurface : ItemsControl
    {
        static ViewingSurface()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewingSurface), new FrameworkPropertyMetadata(typeof(ViewingSurface)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DesignSurfaceItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignSurfaceItem();
        }

    }
}