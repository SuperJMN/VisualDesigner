using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
    public abstract class CanvasItemAdorner : Adorner
    {
        private ICanvasItem canvasItem;

        public CanvasItemAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement)
        {
            CanvasItem = canvasItem;
        }

        protected ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                canvasItem = value;
                this.Left = canvasItem.Left;
                this.Top = canvasItem.Top;
                this.Width = canvasItem.Width;
                this.Height = canvasItem.Height;
            }
        }
    }
}