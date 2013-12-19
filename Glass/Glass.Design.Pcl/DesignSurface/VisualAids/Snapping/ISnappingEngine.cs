using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ISnappingEngine
    {        
        double SnapPoint(double value);
        bool ShouldSnap(Edge edge, double value);
        double Threshold { get; set; }
        ICanvasItem Snappable { get; set; }
        void SetSourceRect(IRect originalRect);
    }
}