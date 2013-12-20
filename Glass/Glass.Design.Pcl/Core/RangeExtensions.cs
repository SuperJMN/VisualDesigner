namespace Glass.Design.Pcl.Core
{
    public static class RangeExtensions
    {
        public static Range Swap(this Range range)
        {
            return new Range(range.SegmentEnd, range.SegmentStart);
        }
    }
}