using System;
using AutoMapper;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using PostSharp.Patterns.Recording;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Drag
{

    public class DragOperationHost
    {
        [NotNull]
        private ICanvasItem ItemToDrag { get; set; }
        [NotNull]
        private IUserInputReceiver FrameOfReference { get; set; }
        [NotNull]
        public ICanvasItemSnappingEngine SnappingEngine { get; set; }

        public object Pointer { get; set; }

        private RecordingScope dragRecordingScope;

        public DragOperationHost(IUserInputReceiver frameOfReference)
        {
            FrameOfReference = frameOfReference;        
            SnappingEngine = new NoEffectsCanvasItemSnappingEngine();
            IsDragging = false;
        }

        private void FrameOfReferenceOnMouseMove(object sender, FingerManipulationEventArgs args)
        {
            if (!IsDragging)
            {
                IsDragging = true;
                OnDragStarted();
            }

            var position = args.GetPosition(FrameOfReference);

            DragOperation.NotifyNewPosition(position);        
        }

        private void FrameOfReferenceOnMouseLeftButtonUp(object sender, FingerManipulationEventArgs args)
        {
            if (DragOperation != null)
            {
                var position = args.GetPosition(FrameOfReference);
                DragOperation.NotifyNewPosition(Mapper.Map<Point>(position));
                FrameOfReference.ReleaseInput(Pointer);
                FrameOfReference.FingerMove -= FrameOfReferenceOnMouseMove;
                DragOperation = null;                
                SnappingEngine.ClearSnappedEdges();

                IsDragging = false;
                Pointer = null;
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

        public void SetDragTarget(IUserInputReceiver hitTestReceiver, ICanvasItem itemToDrag)
        {
            if ( this.dragRecordingScope != null )
            {
                throw new InvalidOperationException("There is already an active drag operation.");         
            }

            this.ItemToDrag = itemToDrag;
            hitTestReceiver.FingerDown += TargetOnPreviewMouseLeftButtonDown;
        }

        private void TargetOnPreviewMouseLeftButtonDown(object sender, FingerManipulationEventArgs fingerManipulationEventArgs)
        {
            fingerManipulationEventArgs.Handled = true;

            Pointer = fingerManipulationEventArgs.Pointer;

            var startingPoint = fingerManipulationEventArgs.GetPosition(FrameOfReference);

            DragOperation = new DragOperation(ItemToDrag, startingPoint, SnappingEngine);

            FrameOfReference.CaptureInput(Pointer);

            FrameOfReference.FingerMove += FrameOfReferenceOnMouseMove;
            FrameOfReference.FingerUp += FrameOfReferenceOnMouseLeftButtonUp;            
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