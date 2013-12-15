using System;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IPositionable
    {
        double Left { get; set; }
        double Top { get; set; }
        
        event EventHandler<LocationChangedEventArgs> LeftChanged;
        event EventHandler<LocationChangedEventArgs> TopChanged;
    }
}