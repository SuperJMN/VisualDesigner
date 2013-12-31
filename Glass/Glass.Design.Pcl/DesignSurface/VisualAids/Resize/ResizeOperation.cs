using System;
using System.Dynamic;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class ResizeOperation
    {
        private ICanvasItem child;

        private ICanvasItem Child
        {
            get { return child; }
            set
            {
                child = value;
            }
        }

        public ResizeHandle ResizeHandle { get; set; }



        [NotNull]
        public ISnappingEngine SnappingEngine { get; set; }

        public ResizeOperation(ICanvasItem child, ResizeHandle resizeResizeHandle, ISnappingEngine snappingEngine)
        {
            Child = child;
            ResizeHandle = resizeResizeHandle;
            SnappingEngine = snappingEngine;

            OriginalRect = child.ToRect();
        }

        public IRect OriginalRect { get; set; }


        public void UpdateHandlePosition(IPoint newPoint)
        {
            var rect = OriginalRect;

            switch (ResizeHandle.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    if (newPoint.X < rect.Right)
                    {
                        rect.SetLeftKeepingRight(newPoint.X);
                    }
                    break;

                case HorizontalAlignment.Right:
                    if (newPoint.X > rect.Left)
                    {
                        rect.SetRightKeepingLeft(newPoint.X);
                    }
                    break;
            }

            switch (ResizeHandle.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    if (newPoint.Y < rect.Bottom)
                    {
                        rect.SetTopKeepingBottom(newPoint.Y);
                    }
                    break;

                case VerticalAlignment.Bottom:
                    if (newPoint.Y > rect.Top)
                    {
                        rect.SetBottomKeepingTop(newPoint.Y);
                    }
                    break;
            }


            Extensions.SetBounds(Child, rect);
        }
    }

    public struct ResizeHandle
    {
        public VerticalAlignment VerticalAlignment { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom,
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right,
    }
}
