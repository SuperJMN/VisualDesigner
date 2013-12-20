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
    }
}