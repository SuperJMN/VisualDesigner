using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using ImpromptuInterface;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Resize
{
    public class WpfUIResizeOperationHandleConnector
    {
        private ICanvasItem CanvasItem { get; set; }
        private IInputElement Parent { get; set; }
        private IEdgeSnappingEngine SnappingEngine { get; set; }

        public WpfUIResizeOperationHandleConnector(ICanvasItem canvasItem, IInputElement parent, IEdgeSnappingEngine snappingEngine)
        {
            CanvasItem = canvasItem;
            Parent = parent;
            SnappingEngine = snappingEngine;
            Handles = new Dictionary<IInputElement, IPoint>();
        }

        private IDictionary<IInputElement, IPoint> Handles { get; set; }
        private ResizeOperation ResizeOperation { get; set; }


        public void RegisterHandle(IInputElement handle, IPoint point)
        {
            Handles.Add(handle, point);
            handle.PreviewMouseLeftButtonDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            mouseButtonEventArgs.Handled = true;

            var inputElement = (IInputElement)sender;

            var handlePoint = Handles[inputElement];

            var absolutePoint = ConvertProportionalToAbsolute(handlePoint);

            ResizeOperation = new ResizeOperation(CanvasItem, absolutePoint, SnappingEngine);            
            Parent.CaptureMouse();

            Parent.MouseMove += ParentOnMouseMove;
            Parent.MouseLeftButtonUp += ParentOnMouseLeftButtonUp;
        }

        private IPoint ConvertProportionalToAbsolute(IPoint handlePoint)
        {
            var x = CanvasItem.Width * handlePoint.X + CanvasItem.Left;
            var y = CanvasItem.Height * handlePoint.Y + CanvasItem.Top;
            return ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
        }

        private void ParentOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (ResizeOperation != null)
            {
                ResizeOperation.UpdateHandlePosition(mouseButtonEventArgs.GetPosition(Parent).ActLike<IPoint>());
                Parent.ReleaseMouseCapture();
                Parent.MouseMove -= ParentOnMouseMove;
                ResizeOperation.Dispose();
                ResizeOperation = null;
                SnappingEngine.ClearSnappedEdges();

                IsDragging = false;
                //OnDragEnd();
            }
        }


        private bool IsDragging { get; set; }

        private void ParentOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var position = mouseEventArgs.GetPosition(Parent);
            var newPoint = position.ActLike<IPoint>();
            ResizeOperation.UpdateHandlePosition(newPoint);

            if (!IsDragging)
            {
                IsDragging = true;
                //OnDragStarted();
            }
        }
    }
}