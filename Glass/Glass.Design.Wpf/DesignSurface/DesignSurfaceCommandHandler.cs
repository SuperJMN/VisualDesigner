using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;
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
            return DesignSurface.SelectedCanvasItems.Any();
        }

        private void BringToFront()
        {
            MoveSelectionTo(DesignSurface.Children.Count - 1);
        }

        private void SendToBack()
        {
            MoveSelectionTo(0);
        }

        private void MoveSelectionTo(int position)
        {
            var idsToMove = new List<int>();

            foreach (var child in DesignSurface.SelectedCanvasItems)
            {
                var childId = DesignSurface.Children.IndexOf(child);
                idsToMove.Add(childId);
            }


            var newIndex = position;
            foreach (var id in idsToMove)
            {
                DesignSurface.Children.Move(id, newIndex);
            }
        }

        private void AlignVertically(VerticalAlignment alignment)
        {
            var aligner = new Aligner(DesignSurface.SelectedCanvasItems);
            aligner.AlignVertically(alignment);
        }

        private void CanExecuteAlignCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanAlign();
        }

        private bool CanAlign()
        {
            return DesignSurface.SelectedCanvasItems.Count > 1;
        }

        private void AlignHorizontally(HorizontalAlignment alignment)
        {
            var aligner = new Aligner(DesignSurface.SelectedCanvasItems);
            aligner.AlignHorizontally(alignment);
        }


        private void CanUngroup(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DesignSurface.SelectedCanvasItems.All(item => item.Children.Any());
        }

        private void Ungroup(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedCanvasItems = DesignSurface.SelectedCanvasItems.ToList();
            foreach (var selectedItem in selectedCanvasItems)
            {
                selectedItem.RemoveAndPromoteChildren();
                DesignSurface.Children.Remove(selectedItem);
            }
        }

        private void CanGroupSelection(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = DesignSurface.SelectedCanvasItems.Count > 1;
        }

        private void Group(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var groupCommandArgs = (GroupCommandArgs)executedRoutedEventArgs.Parameter;
            var group = groupCommandArgs.CreateHostingItem();

            DesignSurface.SelectedCanvasItems.Reparent(group);

            DesignSurface.Children.Add(group);
        }
    }
}