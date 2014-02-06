using System.Collections.Generic;

namespace Glass.Design.Pcl.Canvas
{
    public class CanvasItemSelection : ChildrenExpandableCanvasItem
    {        
        public CanvasItemSelection(IEnumerable<ICanvasItem> children) : base(children)
        {            
            this.ChildrenPositioning = ChildrenPositioning.Absolute;
        }

       
    }
}