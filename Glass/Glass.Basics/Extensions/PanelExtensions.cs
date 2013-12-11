using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Glass.Basics.Extensions
{
    public static class PanelExtensions
    {
        public static List<Visual> GetChildrenWithBounds(this Panel panel, Rect rect)
        {
            var list = new List<Visual>();

            foreach (Visual child in panel.Children)
            {
                var itemBounds = VisualTreeHelper.GetDescendantBounds(child);

                if (!itemBounds.IsEmpty)
                {
                    var containerOffset = VisualTreeHelper.GetOffset(child);

                    itemBounds.Offset(containerOffset);

                    if (rect.Contains(itemBounds))
                        list.Add(child);
                }
            }
            return list;
        } 
    }
}