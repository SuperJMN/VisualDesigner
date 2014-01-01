using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using ImpromptuInterface;
using HorizontalAlignment = Glass.Design.Pcl.DesignSurface.VisualAids.Resize.HorizontalAlignment;
using VerticalAlignment = Glass.Design.Pcl.DesignSurface.VisualAids.Resize.VerticalAlignment;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Resize
{
    public class WpfUIResizeOperationHandleConnector
    {
        private ICanvasItem CanvasItem { get; set; }
        private IInputElement Parent { get; set; }
        private ISnappingEngine SnappingEngine { get; set; }

        public WpfUIResizeOperationHandleConnector(ICanvasItem canvasItem, IInputElement parent, ISnappingEngine snappingEngine)
        {
            CanvasItem = canvasItem;
            Parent = parent;
            SnappingEngine = snappingEngine;
            Handles = new Dictionary<IInputElement, IPoint>();
        }

        private IDictionary<IInputElement, IPoint> Handles { get; set; }
        private ResizeOperation ResizeOperation { get; set; }


        public void RegisterHandler(IInputElement handle, IPoint point)
        {
            Handles.Add(handle, point);
            handle.PreviewMouseLeftButtonDown += HandleOnMouseLeftButtonDown;
        }

        private void HandleOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            mouseButtonEventArgs.Handled = true;

            var inputElement = (IInputElement)sender;

            var handlePoint = Handles[inputElement];

            //handlePoint = ServiceLocator.CoreTypesFactory.CreatePoint(CanvasItem.Left + CanvasItem.Width, 0);
            
            ResizeOperation = new ResizeOperation(CanvasItem, handlePoint, SnappingEngine);
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