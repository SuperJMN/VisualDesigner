using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using ComicDesigner.UIUtils;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.WinRT.Annotations;
using Model;
using PostSharp.Patterns.Recording;
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
            UndoCommand = new RelayCommand( this.Undo, this.CanUndo );
            RedoCommand = new RelayCommand( this.Redo, this.CanRedo );

            RecordingServices.AmbientRecorder.UndoOperations.CollectionChanged += ( sender, args ) => this.UndoCommand.RaiseCanExecuteChanged();
            RecordingServices.AmbientRecorder.RedoOperations.CollectionChanged += (sender, args) => this.RedoCommand.RaiseCanExecuteChanged();
        }

        private void SelectedItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            BringToFrontCommand.RaiseCanExecuteChanged();
            SendToBackCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand SendToBackCommand { get; private set; }

        public RelayCommand BringToFrontCommand { get; private set; }

        private void Undo()
        {
            RecordingServices.AmbientRecorder.Undo();
        }

        private bool CanUndo()
        {
            return RecordingServices.AmbientRecorder.CanUndo;
        }

        private void Redo()
        {
            RecordingServices.AmbientRecorder.Redo();
        }

        private bool CanRedo()
        {
            return RecordingServices.AmbientRecorder.CanRedo;
        }

        private bool IsSomethingSelected()
        {
            return SelectedItems.Count > 0;
        }

        public CanvasItemCollection Items
        {
            get { return EditingContext.Document.Children; }
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
                             FontName = "Comic Sans MS"
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

        public CanvasItemCollection SelectedItems
        {
            get { return EditingContext.SelectedItems; }            
        }

        public RelayCommand LoadItemsCommand { get; set; }

        public IEditingContext EditingContext { get; private set; }
        public RelayCommand UndoCommand { get; private set; }
        public RelayCommand RedoCommand { get; private set; }
    }
}