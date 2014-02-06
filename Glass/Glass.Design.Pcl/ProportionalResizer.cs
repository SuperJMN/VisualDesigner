using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl
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

            if (hookOffset > 0.5)
            {
                positionDelta = -positionDelta;
            }

            return new CanvasItemResizeInfo(positionDelta, sizeDelta);
        }

        public void Resize(ISize newSize)
        {
            var oldSizeVector = canvasItem.GetSize().ToVector();
            var newSizeVector = newSize.ToVector();

            var deltaSize = newSizeVector.Subtract(oldSizeVector);
            DeltaResize(deltaSize);
        }

        public void DeltaResize(IVector resize)
        {
            var horzResize = DeltaResize(resize.X, Anchor.X);
            var vertResize = DeltaResize(resize.Y, Anchor.Y);

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

        public IPoint Anchor { get; set; }        
    }
}