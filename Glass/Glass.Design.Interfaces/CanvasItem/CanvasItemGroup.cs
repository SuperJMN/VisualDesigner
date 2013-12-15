using System.Collections.Generic;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItemGroup : ChildrenExpandableCanvasItem
    {        
        public CanvasItemGroup(IEnumerable<ICanvasItem> children) : base(children)
        {            
        }
    }
}