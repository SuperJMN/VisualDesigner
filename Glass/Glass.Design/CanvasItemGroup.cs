using System.Collections.Generic;
using Design.Interfaces;

namespace Glass.Design
{
    public class CanvasItemGroup : ChildrenExpandableCanvasItem
    {        
        public CanvasItemGroup(IList<ICanvasItem> children) : base(children)
        {            
        }
    }
}