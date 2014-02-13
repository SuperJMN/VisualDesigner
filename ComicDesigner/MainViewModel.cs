using System.Collections.ObjectModel;
using Windows.Storage;
using Glass.Design.Pcl.Canvas;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private CanvasItemCollection items;

        [ImportConstructor]
        public MainViewModel()
        {
            Items = new CanvasItemCollection();
            Entities = new ObservableCollection<Entity>();

            for (int i = 0; i < 5; i++)
            {
                Items.Add(new CanvasRectangle { Left = i * 200, Top = i * 120, Width = 100, Height = 50 });
                Entities.Add(new Entity(i.ToString()));
            }
        }

        public CanvasItemCollection Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Entity> Entities { get; set; }

        public Entity SelectedEntity { get; set; }
    }

    public class Entity
    {
        private string name;
        private bool selected;

        public Entity(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
    }
}