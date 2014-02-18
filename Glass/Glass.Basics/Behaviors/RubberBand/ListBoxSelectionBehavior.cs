using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Glass.Basics.Wpf.Behaviors.RubberBand
{
    public class ListBoxSelectionBehavior : RubberBandSelectionBehavior<ListBox>
    {
        protected override void SetSelectedState(DependencyObject container, bool value)
        {
            var item = (ListBoxItem) container;
            item.IsSelected = value;
        }

        protected override bool GetSelectedState(DependencyObject container)
        {
            var item = (ListBoxItem)container;
            return item.IsSelected;
        }

        protected override void SaveSelectionState()
        {
            var selection = new List<object>(AssociatedObject.SelectedItems.Cast<object>());
            SelectedItemsBackup = new HashSet<object>(selection);
        }

        protected override void Unselect(IEnumerable<DependencyObject> containers)
        {
            AssociatedObject.SelectedItems.Clear();
        }
    }
}