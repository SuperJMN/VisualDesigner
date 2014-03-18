using System;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

namespace Glass.Design.Pcl.Canvas
{
    [Recordable]
    public class CanvasModelItem : CanvasItem
    {
        [Child]
        private readonly CanvasItemCollection items = new CanvasItemCollection();

        [Parent]
        private ICanvasItemContainer parent;

        static CanvasModelItem()
        {
            Recorder = RecordingServices.RecorderProvider.GetRecorder( null );
        }

        public CanvasModelItem()
        {
            // We have to set these members here, and not in the parent class, otherwise
            // the parent class constructor would call the advices on these properties
            // before the aspects have been initialized.
            this.Width = 1;
            this.Height = 1;
        }

        public override CanvasItemCollection Children
        {
            get { return this.items; }
            set { throw new NotSupportedException(); }
        }

        public override double Left { get; set; }
        public override double Top { get; set; }
        public override double Width { get; set; }
        public override double Height { get; set; }

        public override ICanvasItemContainer Parent
        {
            get { return this.parent; }
        }

        public static Recorder Recorder { get; private set; }
    }
}