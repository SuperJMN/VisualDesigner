using System.Collections.Generic;
using System.Linq;

namespace Glass.Design.Pcl.Canvas
{
    public class CanvasItemSelection : ChildrenExpandableCanvasItem
    {
        private readonly string name;

        public CanvasItemSelection(IEnumerable<ICanvasItem> children) : base(children)
        {            
            this.ChildrenPositioning = ChildrenPositioning.Absolute;

            if ( !children.Any() )
            {
                this.name = "empty selection";
            }
            else if ( children.Count() == 1 )
            {
                this.name = string.Join( ", ", children.Select( item => item.GetName() ) );
            }

        }


        public override string GetName()
        {
            return this.name;
        }
    }
}