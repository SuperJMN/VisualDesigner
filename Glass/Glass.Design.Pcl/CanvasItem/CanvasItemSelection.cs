using System.Collections.Generic;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItemSelection : ChildrenExpandableCanvasItem
    {        
        public CanvasItemSelection(IEnumerable<ICanvasItem> children) : base(children)
        {            
            this.ChildrenPositioning = ChildrenPositioning.Absolute;
        }

       
    }
}