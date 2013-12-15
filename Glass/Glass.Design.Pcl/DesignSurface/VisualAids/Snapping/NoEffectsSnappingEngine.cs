using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class NoEffectsSnappingEngine : ISnappingEngine
    {
        public IPoint SnapPoint(IPoint pointToSnap)
        {
            return pointToSnap;
        }
    }
}