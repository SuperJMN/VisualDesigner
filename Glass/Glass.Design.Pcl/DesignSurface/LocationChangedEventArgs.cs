namespace Glass.Design.Pcl.DesignSurface
{
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