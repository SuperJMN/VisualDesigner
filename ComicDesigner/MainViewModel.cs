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
        private CanvasItemViewModel selectedItem;

        [ImportConstructor]
        public MainViewModel(IEditingContext editingContext)
        {
            EditingContext = editingContext;

            // Since we're Main, we have to instance the document into the editing context.
            EditingContext.Document = new Document();

            LoadItemsCommand = new DelegateCommand(OnLoadItems);
        }

        private void OnLoadItems(object parameter)
        {
            var middlePoint = new Point(SurfaceWidth / 2, SurfaceHeight / 2);

            const int marioWidth = 200;
            const int marioHeight = 240;

            var mario = new Mario { Left = middlePoint.X - marioWidth / 2D, Top = middlePoint.Y - marioHeight / 2D, Width = marioWidth, Height = marioHeight };
            var bubble = new Bubble
                         {
                             Left = mario.Right - 70,
                             Top = mario.Top - 170,
                             Width = 250,
                             Height = 280,
                             Text = "WOW. Much decoupled. So AOP. Such Patterns.",
                             Background = new Color(255, 0, 255, 80),
                             TextColor = new Color(255, 0, 0, 0),
                         };

            Items.Add(mario);
            Items.Add(bubble);
        }

        public CanvasItemViewModelCollection Items
        {
            get { return EditingContext.Document.Graphics; }
        }

        public ICommand LoadItemsCommand { get; set; }

        public double SurfaceWidth { get; set; }

        public double SurfaceHeight { get; set; }

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