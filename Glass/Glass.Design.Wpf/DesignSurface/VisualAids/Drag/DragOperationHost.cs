using System;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Drag;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using ImpromptuInterface;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Drag
{

    public class DragOperationHost
    {
        [NotNull]
        private ICanvasItem ItemToDrag { get; set; }
        [NotNull]
        private IInputElement FrameOfReference { get; set; }
        [NotNull]
        public ICanvasItemSnappingEngine SnappingEngine { get; set; }

        public DragOperationHost(IInputElement frameOfReference)
        {
            FrameOfReference = frameOfReference;        
            SnappingEngine = new NoEffectsCanvasItemSnappingEngine();
            IsDragging = false;
        }

        private void FrameOfReferenceOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {           
            var position = mouseEventArgs.GetPosition(FrameOfReference);
            var newPoint = position.ActLike<IPoint>();
            DragOperation.NotifyNewPosition(newPoint);

            if (!IsDragging)
            {
                IsDragging = true;
                OnDragStarted();
            }
        }

        private void InputElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (DragOperation != null)
            {
                var position = mouseButtonEventArgs.GetPosition(FrameOfReference);
                DragOperation.NotifyNewPosition(position.ActLike<IPoint>());
                FrameOfReference.ReleaseMouseCapture();
                FrameOfReference.MouseMove -= FrameOfReferenceOnMouseMove;
                DragOperation = null;                
                SnappingEngine.ClearSnappedEdges();

                IsDragging = false;
                OnDragEnd();
            }
        }

        public DragOperation DragOperation { get; set; }
        public event EventHandler DragStarted;

        protected virtual void OnDragStarted()
        {
            var handler = DragStarted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void SetDragTarget(IInputElement hitTestReceiver, ICanvasItem itemToDrag)
        {
            this.ItemToDrag = itemToDrag;
            hitTestReceiver.PreviewMouseLeftButtonDown += TargetOnPreviewMouseLeftButtonDown;
        }

        private void TargetOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            args.Handled = true;

            var startingPoint = args.GetPosition(FrameOfReference).ActLike<IPoint>();
            DragOperation = new DragOperation(ItemToDrag, startingPoint, SnappingEngine);            

            FrameOfReference.CaptureMouse();

            FrameOfReference.MouseMove += FrameOfReferenceOnMouseMove;
            FrameOfReference.MouseLeftButtonUp += InputElementOnMouseLeftButtonUp;            
        }

        public event EventHandler DragEnd;

        protected virtual void OnDragEnd()
        {
            var handler = DragEnd;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private bool IsDragging { get; set; }
    }
}