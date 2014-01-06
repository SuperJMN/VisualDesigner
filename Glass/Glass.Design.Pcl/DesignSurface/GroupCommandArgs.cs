using System;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface
{
    public class GroupCommandArgs
    {
        public Func<ICanvasItem> CreateCanvasItem { get; set; }
    }
}