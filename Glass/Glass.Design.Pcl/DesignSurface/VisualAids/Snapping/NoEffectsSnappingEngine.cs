using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class NoEffectsSnappingEngine : ISnappingEngine
    {
        public double SnapHorizontal(double value)
        {
            return value;
        }

        public double SnapVertical(double value)
        {
            return value;
        }

        public bool ShouldSnap(Edge edge, double value)
        {
            return false;
        }

        public double Threshold { get; set; }
        public ICanvasItem Snappable { get; set; }
        public void SetSourceRectForResize(IRect originalRect)
        {
            
        }

        public void SetSourceRectForDrag(IRect originalRect)
        {
            
        }
    }
}