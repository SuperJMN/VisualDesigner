using System.Windows;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    class NoEffectsSnappingEngine : ISnappingEngine
    {
        public Point SnapPoint(Point pointToSnap)
        {
            return pointToSnap;
        }
    }
}