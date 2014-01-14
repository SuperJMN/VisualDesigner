using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItemCollection : ObservableCollection<ICanvasItem>
    {
        public CanvasItemCollection()
        {
        }

        public CanvasItemCollection(IEnumerable<ICanvasItem> collection) : base(collection)
        {
        }      
    }
}
