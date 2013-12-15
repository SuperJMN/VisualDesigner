using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    class NoEffectsSnappingEngine : ISnappingEngine
    {
        public IPoint SnapPoint(IPoint pointToSnap)
        {
            return pointToSnap;
        }
    }
}