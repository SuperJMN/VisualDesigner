using Glass.Design.Pcl.Canvas;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

namespace Model
{
    [Recordable]
    public class Document : ICanvasItemContainer
    {
        public Document()
        {
            this.Children = new CanvasItemCollection();
        }

        [Child]
        public CanvasItemCollection Children { get; set; }

     
    }
}