using System;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Drag;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using ImpromptuInterface;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Undo;

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
            var newPoint = position.ActLike<IPoint>();
            DragOperation.NotifyNewPosition(newPoint);

        
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
             StartRecorderOperation();

            var handler = DragStarted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void StartRecorderOperation()
        {
            Recorder recorder = this.ItemToDrag.QueryInterface<IRecordable>(true).Recorder;
            int objectsCount = 1;
            if (recorder == null)
            {
                // This is a selection.
                if (this.ItemToDrag.Children.Count > 0)
                {
                    objectsCount = this.ItemToDrag.Children.Count;
                    recorder = this.ItemToDrag.Children[0].QueryInterface<IRecordable>(true).Recorder;
                    // Assertion: all children have the same recorders, and we don't have to search deep for the recorder.
                }
            }


            if (recorder != null)
            {
                // TODO: Generate a better operation name. We would need to enrich the model, for instance with an object name.
                string operationName = string.Format("Move {0} item(s)", objectsCount);
                this.dragRecordingScope = recorder.StartAtomicOperation(operationName, true);
            }
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

            var startingPoint = args.GetPosition(FrameOfReference).ActLike<IPoint>();
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