using System;

namespace Design.Interfaces
{
    public interface ISizable
    {
        double Width { get; set; }
        double Height { get; set; }
        
        event EventHandler HeightChanged;
        event EventHandler WidthChanged;
    }
}