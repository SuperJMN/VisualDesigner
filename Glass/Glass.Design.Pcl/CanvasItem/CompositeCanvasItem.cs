using PostSharp.Patterns.Model;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CompositeCanvasItem : CanvasItem
    {
        [Surrogate]
        private readonly CanvasItemCollection _items = new CanvasItemCollection();
        public override CanvasItemCollection Children
        {
            get { return this._items; }
        }
    }
}