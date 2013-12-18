namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class NoEffectsSnappingEngine : ISnappingEngine
    {
        public double SnapPoint(double value)
        {
            return value;
        }

        public bool ShouldSnap(Edge edge, double value)
        {
            return false;
        }

        public double Threshold { get; set; }
    }
}