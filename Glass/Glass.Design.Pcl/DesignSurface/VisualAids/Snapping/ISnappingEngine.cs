using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ISnappingEngine
    {        
        double SnapLeft(double value);
        double SnapTop(double value);
        double Threshold { get; set; }
        ICanvasItem Snappable { get; set; }
        void SetSourceRect(IRect originalRect);
    }
}