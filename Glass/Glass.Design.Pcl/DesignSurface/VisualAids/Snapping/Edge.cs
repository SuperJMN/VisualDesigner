using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class Edge
    {
        private readonly Range range;

        public Edge(double axisDistance, Range range, Orientation orientation)
        {
            this.range = range;
            AxisDistance = axisDistance;
            
            Orientation = orientation;            
        }

        public double AxisDistance { get; set; }


        public Orientation Orientation { get; set; }

        public Range Range
        {
            get { return range; }
        }


        protected bool Equals(Edge other)
        {
            return AxisDistance.Equals(other.AxisDistance) && Orientation == other.Orientation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Edge) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (AxisDistance.GetHashCode()*397) ^ (int) Orientation;
            }
        }
    }
}