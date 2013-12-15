using System.Windows;
using System.Windows.Input;
using Glass.Design.Annotations;
using Glass.Design.CanvasItem;
using Glass.Design.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.DesignSurface.VisualAids.Drag
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
        }

        private void FrameOfReferenceOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {            
            DragOperation.NotifyNewPosition(mouseEventArgs.GetPosition(FrameOfReference));            
        }

        private void InputElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (DragOperation != null)
            {
                DragOperation.NotifyNewPosition(mouseButtonEventArgs.GetPosition(FrameOfReference));
                FrameOfReference.ReleaseMouseCapture();
                FrameOfReference.MouseMove -= FrameOfReferenceOnMouseMove;
                DragOperation = null;
            }
        }

        public DragOperation DragOperation { get; set; }

        public void SetDragTarget(IInputElement hitTestReceiver, ICanvasItem itemToDrag)
        {
            this.ItemToDrag = itemToDrag;
            hitTestReceiver.PreviewMouseLeftButtonDown += TargetOnPreviewMouseLeftButtonDown;
        }

        private void TargetOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            args.Handled = true;

            DragOperation = new DragOperation(ItemToDrag, args.GetPosition(FrameOfReference));
            DragOperation.SnappingEngine = SnappingEngine;

            FrameOfReference.CaptureMouse();

            FrameOfReference.MouseMove += FrameOfReferenceOnMouseMove;
            FrameOfReference.MouseLeftButtonUp += InputElementOnMouseLeftButtonUp;            
        }
    }
}