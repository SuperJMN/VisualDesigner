using System;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface ISizable
    {
        double Width { get; set; }
        double Height { get; set; }
        
        event EventHandler<SizeChangeEventArgs> HeightChanged;
        event EventHandler<SizeChangeEventArgs> WidthChanged;
    }
}