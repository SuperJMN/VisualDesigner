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
        public ICanvasItem CanvasItem { get; set; }
        public IInputElement Parent { get; set; }

        public WpfUIResizeOperationHandleConnector(ICanvasItem canvasItem, IInputElement parent)
        {
            CanvasItem = canvasItem;
            Parent = parent;
            Handles = new Dictionary<IInputElement, IPoint>();
        }

        public IDictionary<IInputElement, IPoint> Handles { get; set; }
        public ResizeOperation ResizeOperation { get; set; }


        public void RegisterHandler(IInputElement handle, IPoint point)
        {
            Handles.Add(handle, point);
            handle.PreviewMouseLeftButtonDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            mouseButtonEventArgs.Handled = true;

            var point = Handles[(IInputElement)sender];
            ResizeOperation = new ResizeOperation(CanvasItem, point, new NoEffectsCanvasItemSnappingEngine());
            Parent.CaptureMouse();

            Parent.MouseMove += ParentOnMouseMove;
            Parent.MouseLeftButtonUp += ParentOnMouseLeftButtonUp;
        }

        private void ParentOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (ResizeOperation != null)
            {
                ResizeOperation.UpdateHandlePosition(mouseButtonEventArgs.GetPosition(Parent).ActLike<IPoint>());
                Parent.ReleaseMouseCapture();
                Parent.MouseMove -= ParentOnMouseMove;
                ResizeOperation = null;


                IsDragging = false;
                //OnDragEnd();
            }
        }



        public bool IsDragging { get; set; }

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