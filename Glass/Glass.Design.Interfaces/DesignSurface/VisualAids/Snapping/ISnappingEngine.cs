using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ISnappingEngine
    {
        Point SnapPoint(Point pointToSnap);
    }
}