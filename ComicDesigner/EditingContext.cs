using System.Collections.Generic;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner
{
    [Singleton]
    [Export(typeof(IEditingContext))]
    public class EditingContext : IEditingContext
    {
        [ImportConstructor]
        public EditingContext()
        {
            SelectedItems = new CanvasItemCollection();
        }
        [NotNull]
        public Document Document { get; set; }

        public double SurfaceWidth { get; set; }
        public double SurfaceHeight { get; set; }
        public CanvasItemCollection SelectedItems { get; set; }
    }
}