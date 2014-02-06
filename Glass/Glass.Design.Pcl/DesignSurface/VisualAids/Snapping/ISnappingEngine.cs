using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public interface ISnappingEngine
    {        
        double SnapHorizontal(double value);
        double SnapVertical(double value);
        double Threshold { get; set; }
        ICanvasItem Snappable { get; set; }
        void SetSourceRectForResize(IRect originalRect);
        void SetSourceRectForDrag(IRect originalRect);
    }
}