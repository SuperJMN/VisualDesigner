using System;

namespace Design.Interfaces
{
    public interface IPositionable
    {
        double Left { get; set; }
        double Top { get; set; }
        event EventHandler LocationChanged;
    }
}