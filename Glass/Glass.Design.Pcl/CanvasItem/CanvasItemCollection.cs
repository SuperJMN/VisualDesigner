using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Collections;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItemCollection : AdvisableCollection<ICanvasItem>
    {
        public CanvasItemCollection()
        {
        }

        public CanvasItemCollection(IEnumerable<ICanvasItem> collection) 
        {
            foreach (ICanvasItem canvasItem in collection)
            {
                this.Add(canvasItem);
            }
        }      
    }
}
