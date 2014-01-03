using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public abstract class SnappingEngine : ISnappingEngine
    {
        public SnappingEngine(double thresohold)
        {
            Threshold = thresohold;
        }

        public abstract double SnapHorizontal(double value);
        public abstract double SnapVertical(double value);

        public double Threshold { get; set; }

        [NotNull]
        public ICanvasItem Snappable { get; set; }

        public void SetSourceRectForResize(IRect originalRect)
        {
            SnapHorizontalsForResize(originalRect);
            SnapVerticalsForResize(originalRect);            
        }

        public void SetSourceRectForDrag(IRect originalRect)
        {
            SnapHorizontalsForDrag(originalRect);
            SnapVerticalsForDrag(originalRect);
        }

        private void SnapVerticalsForDrag(IRect originalRect)
        {
            var topSnapped = false;
            var bottomSnapped = false;

            var snappedTop = SnapVertical(originalRect.Top);
            var snappedBottom = SnapVertical(originalRect.Bottom);

            if (originalRect.Top != snappedTop)
            {
                topSnapped = true;
            }
            if (originalRect.Bottom != snappedBottom)
            {
                bottomSnapped = true;
            }

            if (topSnapped)
            {
                Snappable.Top = snappedTop;
                
            }
            else if (bottomSnapped)
            {
                Snappable.Top = snappedBottom - originalRect.Height;
                
            }
            else
            {
                Snappable.Top = originalRect.Top;                
            }
        }

        private void SnapHorizontalsForDrag(IRect originalRect)
        {
            var leftSnapped = false;
            var rightSnapped = false;

            var snappedLeft = SnapHorizontal(originalRect.Left);
            var snappedRight = SnapHorizontal(originalRect.Right);

            if (originalRect.Left != snappedLeft)
            {
                leftSnapped = true;
            }
            if (originalRect.Right != snappedRight)
            {
                rightSnapped = true;
            }

            if (leftSnapped)
            {
                Snappable.Left = snappedLeft;                
            }
            else if (rightSnapped)
            {
                Snappable.Left = snappedRight - originalRect.Width;                

            }
            else
            {
                Snappable.Left = originalRect.Left;                
            }    
        }

        private void SnapHorizontalsForResize(IRect originalRect)
        {
            var leftSnapped = false;
            var rightSnapped = false;

            var snappedLeft = SnapHorizontal(originalRect.Left);
            var snappedRight = SnapHorizontal(originalRect.Right);

            if (originalRect.Left != snappedLeft)
            {
                leftSnapped = true;
            }
            if (originalRect.Right != snappedRight)
            {
                rightSnapped = true;
            }

            if (leftSnapped)
            {
                Snappable.Left = snappedLeft;
                Snappable.Width = originalRect.Right - snappedLeft;
            } else if (rightSnapped)
            {
                Snappable.Left = originalRect.Left;
                Snappable.Width = snappedRight - originalRect.Left;
            }
            else
            {
                Snappable.Left = originalRect.Left;
                Snappable.Width = originalRect.Width;
            }            
        }

        private void SnapVerticalsForResize(IRect originalRect)
        {
            var topSnapped = false;
            var bottomSnapped = false;

            var snappedTop = SnapVertical(originalRect.Top);
            var snappedBottom = SnapVertical(originalRect.Bottom);

            if (originalRect.Top != snappedTop)
            {
                topSnapped = true;
            }
            if (originalRect.Bottom != snappedBottom)
            {
                bottomSnapped = true;
            }

            if (topSnapped)
            {
                Snappable.Top = snappedTop;
                Snappable.Height = originalRect.Bottom - snappedTop;
            }
            else if (bottomSnapped)
            {
                Snappable.Top = originalRect.Top;
                Snappable.Height = snappedBottom - originalRect.Top;
            }
            else
            {
                Snappable.Top = originalRect.Top;
                Snappable.Height = originalRect.Height;
            }
        }
    }
}
