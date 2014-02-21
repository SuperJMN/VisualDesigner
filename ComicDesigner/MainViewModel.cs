using System.Collections.ObjectModel;
using System.Windows.Input;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Core;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private IEditingContext EditingContext { get; set; }
        public IDesignCommandHandler DesignCommandHandler { get; set; }
        private CanvasItemViewModel selectedItem;

        [ImportConstructor]
        public MainViewModel(IEditingContext editingContext, IDesignCommandHandler designCommandHandler)
        {
            EditingContext = editingContext;
            DesignCommandHandler = designCommandHandler;

            // Since we're Main, we have to instance the document into the editing context.
            EditingContext.Document = new Document();            
        }

        public ICommand LoadItemsCommand { get; set; }

        public double SurfaceWidth
        {
            get { return EditingContext.SurfaceWidth; }
            set { EditingContext.SurfaceWidth = value; } 
        }

        public double SurfaceHeight
        {
            get { return EditingContext.SurfaceHeight; }
            set { EditingContext.SurfaceHeight = value; }
        }

        public CanvasItemViewModelCollection Items
        {
            get { return EditingContext.Document.Graphics; }
        }

        public CanvasItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
    }
}