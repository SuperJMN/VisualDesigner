using System;
using Glass.Basics.Wpf.Core;

namespace Glass.Basics.Wpf
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        event EventHandler<EventArgs<bool>> IsSelectedChanged;
    }
}