using System;
using Glass.Basics.Core;

namespace Glass.Basics
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        event EventHandler<EventArgs<bool>> IsSelectedChanged;
    }
}