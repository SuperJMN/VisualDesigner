using System;
using System.Collections;

namespace Glass.Design.DesignSurface
{
    public interface IMultiSelector
    {
        IList SelectedItems { get; }
        event EventHandler<object> ItemSpecified;
        event EventHandler NoneSpecified;
        void UnselectAll();
    }
}