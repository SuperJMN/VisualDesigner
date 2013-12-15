using System.Collections.Generic;

namespace Glass.Design.CanvasItem
{
    public class CanvasItemGroup : ChildrenExpandableCanvasItem
    {        
        public CanvasItemGroup(IEnumerable<ICanvasItem> children) : base(children)
        {            
        }
    }
}