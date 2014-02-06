using System;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.DesignSurface
{
    public class GroupCommandArgs
    {
        public Func<ICanvasItem> CreateHostingItem { get; set; }
    }
}