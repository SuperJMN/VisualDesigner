using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class NoEffectsSnappingEngine : ISnappingEngine
    {
        public double SnapLeft(double value)
        {
            return value;
        }

        public double SnapTop(double value)
        {
            return value;
        }

        public bool ShouldSnap(Edge edge, double value)
        {
            return false;
        }

        public double Threshold { get; set; }
        public ICanvasItem Snappable { get; set; }
        public void SetSourceRect(IRect originalRect)
        {
            
        }
    }
}