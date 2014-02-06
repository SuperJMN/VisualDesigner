using System;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Drag;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using PostSharp.Patterns.Recording;
using Point = Glass.Design.Pcl.Core.Point;

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

        private RecordingScope dragRecordingScope;

        public DragOperationHost(IInputElement frameOfReference)
        {
            FrameOfReference = frameOfReference;        
            SnappingEngine = new NoEffectsCanvasItemSnappingEngine();
            IsDragging = false;
        }

        private void FrameOfReferenceOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (!IsDragging)
            {
                IsDragging = true;
                OnDragStarted();
            }

            var position = mouseEventArgs.GetPosition(FrameOfReference);
            var newPoint = Mapper.Map<Point>(position);
            DragOperation.NotifyNewPosition(newPoint);

        
        }

        private void InputElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (DragOperation != null)
            {
                var position = mouseButtonEventArgs.GetPosition(FrameOfReference);
                DragOperation.NotifyNewPosition(Mapper.Map<Point>(position));
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
             StartRecorderOperation();

            var handler = DragStarted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void StartRecorderOperation()
        {
            // TODO: Generate a better operation name. We would need to enrich the model, for instance with an object name.
            const string operationName = "Move";
            this.dragRecordingScope = CanvasModelItem.Recorder.StartAtomicScope(operationName);
            
        }

       

        public void SetDragTarget(IInputElement hitTestReceiver, ICanvasItem itemToDrag)
        {
            if ( this.dragRecordingScope != null )
                throw new InvalidOperationException("There is already an active drag operation.");


           


            this.ItemToDrag = itemToDrag;
            hitTestReceiver.PreviewMouseLeftButtonDown += TargetOnPreviewMouseLeftButtonDown;
        }

        private void TargetOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            args.Handled = true;

            var startingPoint = Mapper.Map<Point>(args.GetPosition(FrameOfReference));
            DragOperation = new DragOperation(ItemToDrag, startingPoint, SnappingEngine);            

            FrameOfReference.CaptureMouse();

            FrameOfReference.MouseMove += FrameOfReferenceOnMouseMove;
            FrameOfReference.MouseLeftButtonUp += InputElementOnMouseLeftButtonUp;            
        }

        public event EventHandler DragEnd;

        protected virtual void OnDragEnd()
        {
            if (this.dragRecordingScope != null)
            {
                this.dragRecordingScope.Complete();
                this.dragRecordingScope.Dispose();
                this.dragRecordingScope = null;
            }

            var handler = DragEnd;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private bool IsDragging { get; set; }
    }
}