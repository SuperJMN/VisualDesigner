using System.Windows;
using System.Windows.Input;
using Design.Interfaces;

namespace Glass.Design.DesignSurface.VisualAids
{
    internal class DragOperationHost
    {
        private ICanvasItem ItemToDrag { get; set; }
        private IInputElement FrameOfReference { get; set; }

        public DragOperationHost(IInputElement frameOfReference)
        {
            FrameOfReference = frameOfReference;            
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

            FrameOfReference.CaptureMouse();

            FrameOfReference.MouseMove += FrameOfReferenceOnMouseMove;
            FrameOfReference.MouseLeftButtonUp += InputElementOnMouseLeftButtonUp;            
        }
    }
}