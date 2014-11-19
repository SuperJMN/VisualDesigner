using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface;
using PostSharp.Patterns.Recording;
using HorizontalAlignment = Glass.Design.Pcl.Core.HorizontalAlignment;
using VerticalAlignment = Glass.Design.Pcl.Core.VerticalAlignment;


namespace Glass.Design.Wpf.DesignSurface
{
    public sealed class DesignSurfaceCommandHandler
    {
        private IDesignSurface DesignSurface { get; set; }
        private UIElement UIElement { get; set; }


        public DesignSurfaceCommandHandler(IDesignSurface designSurface, UIElement uiElement)
        {
            DesignSurface = designSurface;
            UIElement = uiElement;

            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.GroupCommand, Group, CanGroupSelection));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.PromoteChildrenCommand, Ungroup, CanUngroup));

            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignHorizontallyLeftCommand,
                (sender, args) => AlignHorizontally(HorizontalAlignment.Left), CanExecuteAlignCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignHorizontallyCenterCommand,
                (sender, args) => AlignHorizontally(HorizontalAlignment.Center), CanExecuteAlignCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignHorizontallyRightCommand,
                (sender, args) => AlignHorizontally(HorizontalAlignment.Right), CanExecuteAlignCommandHandler));

            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyTopCommand,
                (sender, args) => AlignVertically(VerticalAlignment.Top), CanExecuteAlignCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyCenterCommand,
                (sender, args) => AlignVertically(VerticalAlignment.Center), CanExecuteAlignCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyBottomCommand,
                (sender, args) => AlignVertically(VerticalAlignment.Bottom), CanExecuteAlignCommandHandler));

            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.BringToFrontCommand,
                (sender, args) => BringToFront(), IsSomethingSelectedCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.SendToBackCommand,
               (sender, args) => SendToBack(), IsSomethingSelectedCommandHandler));
        }

        private void IsSomethingSelectedCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsSomethingSelected();
        }

        private bool IsSomethingSelected()
        {
            return DesignSurface.SelectedItems.Count > 0;
        }

        private void BringToFront()
        {
            MoveSelectionTo(DesignSurface.CanvasDocument.Children.Count - 1);
        }

        private void SendToBack()
        {
            MoveSelectionTo(0);
        }

        private void MoveSelectionTo(int position)
        {
            var idsToMove = new List<int>();

            foreach (ICanvasItem child in DesignSurface.SelectedItems)
            {
                var childId = DesignSurface.CanvasDocument.Children.IndexOf(child);
                idsToMove.Add(childId);
            }


            var newIndex = position;
            foreach (var id in idsToMove)
            {
                DesignSurface.CanvasDocument.Children.Move(id, newIndex);
            }
        }

        private void AlignVertically(VerticalAlignment alignment)
        {
            var aligner = new Aligner(DesignSurface.SelectedItems.Cast<ICanvasItem>().ToList());
            aligner.AlignVertically(alignment);
        }

        private void CanExecuteAlignCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanAlign();
        }

        private bool CanAlign()
        {
            return DesignSurface.SelectedItems.Count > 1;
        }

        private void AlignHorizontally(HorizontalAlignment alignment)
        {
            var aligner = new Aligner(DesignSurface.GetSelectedCanvasItems());
            aligner.AlignHorizontally(alignment);
        }


        private void CanUngroup(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DesignSurface.GetSelectedCanvasItems().All(item => item.Children.Any());
        }

        private void Ungroup(object sender, ExecutedRoutedEventArgs e)
        {
            List<ICanvasItem> selectedCanvasItems = DesignSurface.GetSelectedCanvasItems().ToList();


            using ( RecordingServices.DefaultRecorder.OpenScope("Ungroup") )
            {
                foreach (var selectedItem in selectedCanvasItems)
                {
                    selectedItem.RemoveAndPromoteChildren();
                    this.DesignSurface.CanvasDocument.Children.Remove(selectedItem);
                }
            }
        }

        private void CanGroupSelection(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = DesignSurface.SelectedItems.Count > 1;
        }

        private void Group(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {

            var groupCommandArgs = (GroupCommandArgs)executedRoutedEventArgs.Parameter;

            IEnumerable<ICanvasItem> items = DesignSurface.GetSelectedCanvasItems().ToList();
            ICanvasItem group = groupCommandArgs.CreateHostingItem();

            using ( RecordingServices.DefaultRecorder.OpenScope("Group") )
            {
                // We have to *first* add the group to the document to make it recordable.
                this.DesignSurface.CanvasDocument.Children.Add(@group);

                items.Reparent(@group);
            }
        }
    }
}