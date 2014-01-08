using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;

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

            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.GroupCommand, GroupSelection, CanGroupSelection));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.PromoteChildrenCommand, UngroupSelection, CanUngroupSelection));
            UIElement.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.AlignVerticallyCommand, AlignSelection, CanAlignSelection));
        }

        private void CanAlignSelection(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DesignSurface.SelectedCanvasItems.Count > 1;
        }

        private void AlignSelection(object sender, ExecutedRoutedEventArgs e)
        {
            var aligner = new Aligner(DesignSurface.SelectedCanvasItems);
            aligner.AlignLeft();
        }


        private void CanUngroupSelection(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DesignSurface.SelectedCanvasItems.All(item => Enumerable.Any<ICanvasItem>(item.Children));
        }

        private void UngroupSelection(object sender, ExecutedRoutedEventArgs e)
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

        private void GroupSelection(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var groupCommandArgs = (GroupCommandArgs) executedRoutedEventArgs.Parameter;
            var group = groupCommandArgs.CreateHostingItem();

            DesignSurface.SelectedCanvasItems.Move(group);

            DesignSurface.Children.Add(group);
        }
    }
}