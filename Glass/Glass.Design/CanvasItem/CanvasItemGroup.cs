using System.Collections.Generic;
using Design.Interfaces;

namespace Glass.Design.CanvasItem
{
    public class CanvasItemGroup : ChildrenExpandableCanvasItem
    {        
        public CanvasItemGroup(IEnumerable<ICanvasItem> children) : base(children)
        {            
        }
    }
}