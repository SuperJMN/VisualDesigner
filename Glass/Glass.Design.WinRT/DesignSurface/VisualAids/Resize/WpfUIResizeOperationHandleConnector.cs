using System.Collections.Generic;
using Windows.Devices.Input;
using Windows.UI.Xaml;
using AutoMapper;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
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
            //handle.PreviewMouseLeftButtonDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            //mouseButtonEventArgs.Handled = true;

            var inputElement = (IUIElement)sender;

            var handlePoint = Handles[inputElement];

            var absolutePoint = ConvertProportionalToAbsolute(handlePoint);

            ResizeOperation = new ResizeOperation(CanvasItem, absolutePoint, SnappingEngine);
            //Parent.CaptureMouse();

            //Parent.MouseMove += ParentOnMouseMove;
            //Parent.MouseLeftButtonUp += ParentOnMouseLeftButtonUp;
        }

        private IPoint ConvertProportionalToAbsolute(IPoint handlePoint)
        {
            var x = CanvasItem.Width * handlePoint.X + CanvasItem.Left;
            var y = CanvasItem.Height * handlePoint.Y + CanvasItem.Top;
            return new Point(x, y);
        }

        private void ParentOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (ResizeOperation != null)
            {
                var newPoint = Mapper.Map<Point>(mouseButtonEventArgs.GetPosition(Parent));
                ResizeOperation.UpdateHandlePosition(newPoint);
                //Parent.ReleaseMouseCapture();
                //Parent.MouseMove -= ParentOnMouseMove;
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
            //var position = mouseEventArgs.GetPosition(Parent);
            //var newPoint = position.ActLike<IPoint>();
            //ResizeOperation.UpdateHandlePosition(newPoint);

            if (!IsDragging)
            {
                IsDragging = true;
                //OnDragStarted();
            }
        }
    }

    internal class MouseButtonEventArgs
    {
        public Point GetPosition(IUIElement parent)
        {
            throw new System.NotImplementedException();
        }

        public bool Handled { get; set; }
    }
}