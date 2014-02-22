using System.Collections.Generic;
using Glass.Design.Pcl.Annotations;
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
            SelectedItems = new CanvasItemViewModelCollection();
        }
        [NotNull]
        public Document Document { get; set; }

        public double SurfaceWidth { get; set; }
        public double SurfaceHeight { get; set; }
        public CanvasItemViewModelCollection SelectedItems { get; set; }
    }
}