using System;

namespace Design.Interfaces
{
    public interface IPositionable
    {
        double Left { get; set; }
        double Top { get; set; }
        
        event EventHandler<LocationChangedEventArgs> LeftChanged;
        event EventHandler<LocationChangedEventArgs> TopChanged;
    }

    public class LocationChangedEventArgs
    {
        public LocationChangedEventArgs()
        {
            
        }
        public LocationChangedEventArgs(double oldValue, double newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public double OldValue { get; set; }
        public double NewValue { get; set; }
    }
     
}