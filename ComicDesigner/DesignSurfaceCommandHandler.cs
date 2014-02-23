using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using ComicDesigner.UIUtils;
using Glass.Design.Pcl.Core;
using Glass.Design.WinRT.Annotations;
using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner
{
    [Export(typeof(IDesignCommandHandler)), UsedImplicitly]
    public class DesignSurfaceCommandHandler : IDesignCommandHandler
    {
        [ImportConstructor]
        public DesignSurfaceCommandHandler(IEditingContext editingContext)
        {
            EditingContext = editingContext;

            EditingContext.SelectedItems.CollectionChanged += SelectedItemsOnCollectionChanged;

            LoadItemsCommand = new RelayCommand(OnLoadItems);
            BringToFrontCommand = new RelayCommand(BringToFront, IsSomethingSelected);
            SendToBackCommand = new RelayCommand(SendToBack, IsSomethingSelected);
        }

        private void SelectedItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            BringToFrontCommand.RaiseCanExecuteChanged();
            SendToBackCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand SendToBackCommand { get; set; }

        public RelayCommand BringToFrontCommand { get; set; }

        private bool IsSomethingSelected()
        {
            return SelectedItems.Count > 0;
        }

        public CanvasItemViewModelCollection Items
        {
            get { return EditingContext.Document.Graphics; }
        }

        private void OnLoadItems()
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
                             FontSize = 16D,
                         };

            Items.Add(mario);
            Items.Add(bubble);
        }

        private void BringToFront()
        {
            MoveSelectionTo(Items.Count - 1);
        }

        private void SendToBack()
        {
            MoveSelectionTo(0);
        }

        private void MoveSelectionTo(int position)
        {
            var idsToMove = new List<int>();

            foreach (var child in SelectedItems)
            {
                var childId = Items.IndexOf(child);
                idsToMove.Add(childId);
            }


            var newIndex = position;
            foreach (var id in idsToMove)
            {
                Items.Move(id, newIndex);
            }
        }

        public CanvasItemViewModelCollection SelectedItems
        {
            get { return EditingContext.SelectedItems; }            
        }

        public RelayCommand LoadItemsCommand { get; set; }

        public IEditingContext EditingContext { get; private set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; private set; }
    }
}