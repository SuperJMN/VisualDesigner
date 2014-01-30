using PostSharp.Patterns.Model;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CompositeCanvasItem : CanvasItem
    {
        [Surrogate]
        private readonly CanvasItemCollection children = new CanvasItemCollection();
        public override CanvasItemCollection Children
        {
            get { return this.children; }
        }
    }
}