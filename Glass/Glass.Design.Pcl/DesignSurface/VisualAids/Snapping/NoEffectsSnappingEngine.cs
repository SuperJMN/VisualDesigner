using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    class NoEffectsSnappingEngine : ISnappingEngine
    {
        public Point SnapPoint(Point pointToSnap)
        {
            return pointToSnap;
        }
    }
}