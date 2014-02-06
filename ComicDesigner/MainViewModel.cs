using Windows.Foundation;
using PostSharp.Patterns.Collections;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private AdvisableCollection<ModelObject> items;

        private Rect p;

        [ImportConstructor]
        public MainViewModel()
        {
            Items = new AdvisableCollection<ModelObject>();
            for (int i = 0; i < 5; i++)
            {
                Items.Add(new ModelObject());
            }
        }

        public AdvisableCollection<ModelObject> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }
    }

    public class ModelObject
    {
    }
}