using System.Collections.Generic;
using AutoMapper;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class UIResizeOperationHandleConnector
    {
        private ICanvasItem CanvasItem { get; set; }
        private IUserInputReceiver Parent { get; set; }
        private IEdgeSnappingEngine SnappingEngine { get; set; }

        public UIResizeOperationHandleConnector(ICanvasItem canvasItem, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {
            CanvasItem = canvasItem;
            Parent = parent;
            SnappingEngine = snappingEngine;
            Handles = new Dictionary<IUserInputReceiver, IPoint>();
        }

        private IDictionary<IUserInputReceiver, IPoint> Handles { get; set; }
        private ResizeOperation ResizeOperation { get; set; }


        public void RegisterHandle(IUserInputReceiver handle, IPoint point)
        {
            Handles.Add(handle, point);
            handle.FingerDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, FingerManipulationEventArgs args)
        {
            args.Handled = true;

            var inputElement = (IUserInputReceiver)sender;

            var handlePoint = Handles[inputElement];

            var absolutePoint = ConvertProportionalToAbsolute(handlePoint);

            ResizeOperation = new ResizeOperation(CanvasItem, absolutePoint, SnappingEngine);            
            Parent.CaptureInput(null);

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
                var position = args.GetPosition(Parent);
                ResizeOperation.UpdateHandlePosition(position);
                Parent.ReleaseInput(null);
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
            var position = args.GetPosition(Parent);
            var parentPositon = ((IUIElement) Parent).GetPosition();
            var finalPoint = position.Add(parentPositon);

            var newPoint = Mapper.Map<Point>(finalPoint);
            ResizeOperation.UpdateHandlePosition(newPoint);

            if (!IsDragging)
            {
                IsDragging = true;
                //OnDragStarted();
            }
        }
    }
}