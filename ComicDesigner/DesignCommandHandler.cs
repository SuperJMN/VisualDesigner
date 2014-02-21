using System.Windows.Input;
using Glass.Design.Pcl.Core;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export(typeof(IDesignCommandHandler))]
    public class DesignCommandHandler : IDesignCommandHandler
    {
        [ImportConstructor]
        public DesignCommandHandler(IEditingContext editingContext)
        {
            EditingContext = editingContext;
            LoadItemsCommand = new DelegateCommand(OnLoadItems);
        }

        public CanvasItemViewModelCollection Items
        {
            get { return EditingContext.Document.Graphics; }
        }

        private void OnLoadItems(object parameter)
        {
            var middlePoint = new Point(EditingContext.SurfaceWidth / 2, EditingContext.SurfaceHeight / 2);

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

        public ICommand LoadItemsCommand { get; set; }

        public IEditingContext EditingContext { get; private set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; private set; }
    }
}