namespace Glass.Design.Pcl.Core
{
    public class Range
    {
        public Range(double segmentStart, double segmentEnd)
        {
            SegmentStart = segmentStart;
            SegmentEnd = segmentEnd;
        }

        public double SegmentStart { get; set; }
        public double SegmentEnd { get; set; }

        protected bool Equals(Range other)
        {
            return SegmentStart.Equals(other.SegmentStart) && SegmentEnd.Equals(other.SegmentEnd);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Range) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SegmentStart.GetHashCode()*397) ^ SegmentEnd.GetHashCode();
            }
        }
    }
}