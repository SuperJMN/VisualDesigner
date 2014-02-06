namespace Glass.Design.Pcl.Canvas
{
    public class CanvasVisualItem : CanvasItem
    {
        private readonly CanvasItemCollection children;

        public CanvasVisualItem()
        {
            this.children = new CanvasItemCollection();
            this.Width = 1;
            this.Height = 1;
        }

        public override CanvasItemCollection Children { get { return this.children; }}

        public override double Left { get; set; }
        public override double Top { get; set; }
        public override double Width { get; set; }
        public override double Height { get; set; }
    }
}