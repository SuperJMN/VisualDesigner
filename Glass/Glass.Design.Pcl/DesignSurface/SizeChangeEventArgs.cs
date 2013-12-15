using System;

namespace Glass.Design.Pcl.DesignSurface
{
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