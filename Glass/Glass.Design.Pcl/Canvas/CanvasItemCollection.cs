using System.Collections.Generic;
using PostSharp.Patterns.Collections;

namespace Glass.Design.Pcl.Canvas
{
    public class CanvasItemCollection : AdvisableCollection<ICanvasItem>
    {
        public CanvasItemCollection()
        {
        }

        public CanvasItemCollection(IEnumerable<ICanvasItem> collection) : base(collection)
        {
        }

      
    }
}
