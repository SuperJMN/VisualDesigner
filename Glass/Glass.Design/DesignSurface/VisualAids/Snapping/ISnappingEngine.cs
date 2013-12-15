using System.Windows;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    public interface ISnappingEngine
    {
        Point SnapPoint(Point pointToSnap);
    }
}