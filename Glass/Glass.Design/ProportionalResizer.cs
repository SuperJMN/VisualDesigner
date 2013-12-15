using System.Windows;
using Glass.Design.CanvasItem;

namespace Glass.Design
{
    public class ProportionalResizer
    {
        private readonly ICanvasItem canvasItem;

        public ProportionalResizer(ICanvasItem canvasItem)
        {
            this.canvasItem = canvasItem;
        }

        private static CanvasItemResizeInfo DeltaResize(double delta, double hookOffset)
        {
            var sizeDelta = delta * (1 - hookOffset);
            var positionDelta = delta - sizeDelta;
            return new CanvasItemResizeInfo(positionDelta, sizeDelta);
        }

        public void DeltaResize(Vector resize)
        {
            var horzResize = DeltaResize(resize.X, HookPoint.X);
            var vertResize = DeltaResize(resize.Y, HookPoint.Y);

            canvasItem.Left += horzResize.PositionDelta;

            if (canvasItem.Width + horzResize.SizeDelta - horzResize.PositionDelta >= 0)
            {
                canvasItem.Width += horzResize.SizeDelta - horzResize.PositionDelta;
            }

            canvasItem.Top += vertResize.PositionDelta;
            if (canvasItem.Height + vertResize.SizeDelta - vertResize.PositionDelta >= 0)
            {
                canvasItem.Height += vertResize.SizeDelta - vertResize.PositionDelta;
            }
        }

        public Vector HookPoint { get; set; }
    }
}