using System.Collections.Generic;
using System.Threading;
using Glass.Design.Pcl.CanvasItem;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Undo;

namespace SampleModel
{
    public class CanvasItemModel : CompositeCanvasItem
    {
        private static int nextId;
        private readonly int id = Interlocked.Increment(ref nextId);

        public CanvasItemModel()
        {
            this.Name = this.GetType().Name + id;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            // For better debugging.
            return string.Format("{0} #{5}, Left={1}, Top={2}, Width={3}, Height={4}", this.GetType().Name, this.Left,
                this.Top, this.Width, this.Height, this.id);
        }
    }


    [Recordable]
    public class CanvasModel
    {
        public CanvasModel()
        {
            this.Items = new CanvasItemCollection();
        }

        public CanvasModel(IEnumerable<ICanvasItem> items )
        {
            this.Items = new CanvasItemCollection(items);
        }

        [Surrogate]
        public CanvasItemCollection Items { get; private set; }   
    }
}