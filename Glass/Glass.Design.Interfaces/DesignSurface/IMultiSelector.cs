using System;
using System.Collections;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IMultiSelector
    {
        IList SelectedItems { get; }
        event EventHandler<object> ItemSpecified;
        event EventHandler NoneSpecified;
        void UnselectAll();
    }
}