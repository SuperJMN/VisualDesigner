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
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyMiddleCommand,
                (sender, args) => AlignVertically(VerticalAlignment.Middle), CanExecuteAlignCommandHandler));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyBottomCommand,
                (sender, args) => AlignVertically(VerticalAlignment.Bottom), CanExecuteAlignCommandHandler));
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

            DesignSurface.SelectedCanvasItems.Move(group);

            DesignSurface.Children.Add(group);
        }
    }
}