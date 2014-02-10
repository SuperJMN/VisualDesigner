using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Glass.Basics.Wpf.Extensions;

namespace Glass.Basics.Wpf.Behaviors.DragDrop
{
    public static class ChildrenRetriever
    {
        public static IEnumerable<UIElement> GetChildrenFromPanel(Panel panel)
        {
            return panel.Children.Cast<UIElement>();
        }

        public static IEnumerable<UIElement> GetChildrenFromItemsControl(ItemsControl itemsControl)
        {
            var items = itemsControl.Items.Cast<object>();
            var itemContainers = items.Select(o => (UIElement)itemsControl.ItemContainerGenerator.ContainerFromItem(o));
            return itemContainers;
        }

        public static UIElement GetChildAt(IEnumerable<UIElement> children, Point point)
        {
            var child = children.FirstOrDefault(element => element.GetRectRelativeToParent().Contains(point));
            return child;
        }

        public static UIElement GetChildAt(UIElementCollection children, Point point)
        {            
            return GetChildAt(children.Cast<UIElement>(), point);
        }
    }
}