using System;

namespace Glass.Design.DesignSurface
{
    public interface ISizable
    {
        double Width { get; set; }
        double Height { get; set; }
        
        event EventHandler<SizeChangeEventArgs> HeightChanged;
        event EventHandler<SizeChangeEventArgs> WidthChanged;
    }

    public class SizeChangeEventArgs : EventArgs
    {
        public SizeChangeEventArgs()
        {
        }

        public SizeChangeEventArgs(double oldValue, double newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public double OldValue { get; set; }
        public double NewValue { get; set; }
    }
}