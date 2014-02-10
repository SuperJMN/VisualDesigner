using System.Windows;
using System.Windows.Controls;

namespace Glass.Basics.Wpf.Presentation
{
    public static class ItemsControlExtensions
    {
        public static DependencyObject GetContainer(this ItemsControl itemsControl, object item)
        {
            return itemsControl.IsItemItsOwnContainer(item)
                ? (DependencyObject) item
                : itemsControl.ItemContainerGenerator.ContainerFromItem(item);
        }
    }
}