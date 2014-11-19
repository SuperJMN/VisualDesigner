using System;

namespace Glass.Design.Pcl.Canvas
{
    public abstract class CanvasVisualItem : CanvasItem
    {
        private readonly CanvasItemCollection children;

        protected CanvasVisualItem()
        {
            this.children = new CanvasItemCollection();
            this.Width = 1;
            this.Height = 1;
        }

        public override CanvasItemCollection Children
        {
            get { return this.children; }
            set { throw new System.NotImplementedException(); }
        }

        public override double Left { get; set; }
        public override double Top { get; set; }
        public override double Width { get; set; }
        public override double Height { get; set; }

        public override ICanvasItemContainer Parent
        {
            get { throw new NotSupportedException(); }
        }
    }
}