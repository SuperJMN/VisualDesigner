using System.Collections.Generic;
using Windows.Devices.Input;
using AutoMapper;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using IUIElement = Glass.Design.Pcl.PlatformAbstraction.IUIElement;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Resize
{
    public class WpfUIResizeOperationHandleConnector
    {
        private ICanvasItem CanvasItem { get; set; }
        private IUIElement Parent { get; set; }
        private IEdgeSnappingEngine SnappingEngine { get; set; }

        public WpfUIResizeOperationHandleConnector(ICanvasItem canvasItem, IUIElement parent, IEdgeSnappingEngine snappingEngine)
        {
            CanvasItem = canvasItem;
            Parent = parent;
            SnappingEngine = snappingEngine;
            Handles = new Dictionary<IUIElement, IPoint>();
        }

        private IDictionary<IUIElement, IPoint> Handles { get; set; }
        private ResizeOperation ResizeOperation { get; set; }


        public void RegisterHandle(IUIElement handle, IPoint point)
        {
            Handles.Add(handle, point);
            handle.FingerDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, FingerManipulationEventArgs args)
        {
            //mouseButtonEventArgs.Handled = true;

            var inputElement = (IUIElement)sender;

            var handlePoint = Handles[inputElement];

            var absolutePoint = ConvertProportionalToAbsolute(handlePoint);

            ResizeOperation = new ResizeOperation(CanvasItem, absolutePoint, SnappingEngine);
            Parent.CaptureInput();

            Parent.FingerMove += ParentOnMouseMove;
            Parent.FingerUp += ParentOnMouseLeftButtonUp;
        }

        private IPoint ConvertProportionalToAbsolute(IPoint handlePoint)
        {
            var x = CanvasItem.Width * handlePoint.X + CanvasItem.Left;
            var y = CanvasItem.Height * handlePoint.Y + CanvasItem.Top;
            return new Point(x, y);
        }

        private void ParentOnMouseLeftButtonUp(object sender, FingerManipulationEventArgs args)
        {
            if (ResizeOperation != null)
            {
                var newPoint = args.Point;
                ResizeOperation.UpdateHandlePosition(newPoint);
                Parent.ReleaseInput();
                Parent.FingerMove -= ParentOnMouseMove;
                ResizeOperation.Dispose();
                ResizeOperation = null;
                SnappingEngine.ClearSnappedEdges();

                IsDragging = false;
                //OnDragEnd();
            }
        }


        private bool IsDragging { get; set; }

        private void ParentOnMouseMove(object sender, FingerManipulationEventArgs args)
        {
            var point = args.Point;

            ResizeOperation.UpdateHandlePosition(point);

            if (!IsDragging)
            {
                IsDragging = true;
                //OnDragStarted();
            }
        }
    }

    
}