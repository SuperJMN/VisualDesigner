using System;
using System.Collections;

namespace Glass.Design.DesignSurface.VisualAids.Selection
{
    public class SelectionHandler
    {
        private readonly IMultiSelector multiSelector;

        private IList SelectedItems
        {
            get { return multiSelector.SelectedItems; }
        }

        public SelectionHandler(IMultiSelector multiSelector)
        {
            this.multiSelector = multiSelector;
            multiSelector.ItemSpecified += MultiSelectorOnItemSpecified;
            multiSelector.NoneSpecified += MultiSelectorOnNoneSpecified;
            SelectionMode = SelectionMode.Direct;
        }

        private void MultiSelectorOnItemSpecified(object sender, object item)
        {
            
            switch (SelectionMode)
            {
                case SelectionMode.Add:
                    SelectedItems.Add(item);
                    break;
                case SelectionMode.Direct:
                    multiSelector.UnselectAll();
                    SelectedItems.Add(item);
                    break;
                case SelectionMode.Invert:
                    Invert(item);
                    break;
                case SelectionMode.Subtract:
                    SelectedItems.Remove(item);
                    break;
            }
        }

        private void Invert(object o)
        {
            if (SelectedItems.Contains(o))
            {
                SelectedItems.Remove(o);
            }
            else
            {
                SelectedItems.Add(o);
            }
        }

        private void MultiSelectorOnNoneSpecified(object sender, EventArgs eventArgs)
        {
            multiSelector.UnselectAll();
        }

        public SelectionMode SelectionMode { get; set; }
    }
}