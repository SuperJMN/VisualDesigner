using System;
using System.Collections;
using System.Collections.Generic;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IMultiSelector
    {
        IList<object> SelectedItems { get; }
        event EventHandler<object> ItemSpecified;
        event EventHandler SelectionCleared;
        void UnselectAll();
    }
}