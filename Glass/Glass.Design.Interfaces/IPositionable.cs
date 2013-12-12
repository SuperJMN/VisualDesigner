using System;
using System.ComponentModel;

namespace Design.Interfaces
{
    public interface IPositionable
    {
        double Left { get; set; }
        double Top { get; set; }
        
        event EventHandler<LocationChangedEventArgs> LeftChanged;
        event EventHandler<LocationChangedEventArgs> TopChanged;
        void SetTopCoercionMethod(CoercionHandler coerceMethod);
        void SetLeftCoercionMethod(CoercionHandler coerceMethod);
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

    public delegate object CoercionHandler(object desiredValue);

    
}