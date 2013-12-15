using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IResizeThumb : ICanvasItem
    {
        ICanvasItem CanvasItem { get; set; }
        void DeltaMove(double horizontalChange, double verticalChange);

        bool AllowVerticalResize { get; set; }
        bool AllowHorizontalResize { get; set; }
    }
}