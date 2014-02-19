using System.Collections.Generic;
using Glass.Design.Pcl.Canvas;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

namespace SampleModel
{
    [Recordable]
    public class CanvasDocument : ICanvasItemContainer
    {
        public CanvasDocument()
        {
            this.Children = new CanvasItemCollection();
        }

        public CanvasDocument(IEnumerable<ICanvasItem> items )
        {
            this.Children = new CanvasItemCollection(items);
        }

        [Child]
        public CanvasItemCollection Children { get; set; }

    }
}