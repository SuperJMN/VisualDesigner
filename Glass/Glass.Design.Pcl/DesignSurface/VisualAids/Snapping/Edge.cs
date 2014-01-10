using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class Edge
    {
        public Edge(double axisDistance, Range range, Orientation orientation)
        {
            this.Range = range;
            AxisDistance = axisDistance;
            
            Orientation = orientation;            
        }

        public double AxisDistance { get; set; }


        public Orientation Orientation { get; set; }

        public Range Range { get; private set; }

        public Edge(Range range, Orientation orientation, double axisDistance)
        {
            Range = range;
            Orientation = orientation;
            AxisDistance = axisDistance;
        }
    }
}