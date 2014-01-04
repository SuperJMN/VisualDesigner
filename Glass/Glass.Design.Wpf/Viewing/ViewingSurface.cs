using System.Windows;
using System.Windows.Controls;
using Glass.Design.Wpf.DesignSurface;

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
            return item is DesignerItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignerItem();
        }

    }
}