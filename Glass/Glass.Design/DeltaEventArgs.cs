namespace Glass.Design
{
    public class DeltaEventArgs
    {
        public DeltaEventArgs(double horizontalChange, double verticalChange)
        {
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }

        public double HorizontalChange { get; set; }
        public double VerticalChange { get; set; }
    }
}