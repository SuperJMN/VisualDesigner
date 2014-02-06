using System.Collections.Generic;
using System.Linq;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface;
using HorizontalAlignment = Glass.Design.Pcl.Core.HorizontalAlignment;
using VerticalAlignment = Glass.Design.Pcl.Core.VerticalAlignment;
using IUIElement = Glass.Design.Pcl.PlatformAbstraction.IUIElement;

namespace Glass.Design.WinRT
{
    public sealed class DesignSurfaceCommandHandler
    {
        private IDesignSurface DesignSurface { get; set; }
        private IUIElement IUIElement { get; set; }


        public DesignSurfaceCommandHandler(IDesignSurface designSurface, IUIElement uiElement)
        {
            DesignSurface = designSurface;
            IUIElement = uiElement;

            
        }

        //private void IsSomethingSelectedCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = IsSomethingSelected();
        //}

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

        //private void CanExecuteAlignCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = CanAlign();
        //}

        private bool CanAlign()
        {
            return DesignSurface.SelectedItems.Count > 1;
        }

        private void AlignHorizontally(HorizontalAlignment alignment)
        {
            var aligner = new Aligner(DesignSurface.GetSelectedCanvasItems());
            aligner.AlignHorizontally(alignment);
        }


        //private void CanUngroup(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = DesignSurface.GetSelectedCanvasItems().All(item => item.Children.Any());
        //}

        //private void Ungroup(object sender, ExecutedRoutedEventArgs e)
        //{
        //    List<ICanvasItem> selectedCanvasItems = DesignSurface.GetSelectedCanvasItems().ToList();

        //    Recorder recorder = selectedCanvasItems.GetRecorder();

        //    using (RecordingScope recordingScope = recorder.StartAtomicScope("Ungroup"))
        //    {

        //        foreach (var selectedItem in selectedCanvasItems)
        //        {
        //            selectedItem.RemoveAndPromoteChildren();
        //            DesignSurface.CanvasDocument.Children.Remove(selectedItem);
        //        }

        //        recordingScope.Complete();
               
        //    }


        //}

        //private void CanGroupSelection(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        //{
        //    canExecuteRoutedEventArgs.CanExecute = DesignSurface.SelectedItems.Count > 1;
        //}

        //private void Group(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        //{

        //    var groupCommandArgs = (GroupCommandArgs)executedRoutedEventArgs.Parameter;

        //    IEnumerable<ICanvasItem> items = DesignSurface.GetSelectedCanvasItems().ToList();
        //    ICanvasItem group = groupCommandArgs.CreateHostingItem();

        //    Recorder recorder = items.GetRecorder();

        //    using (RecordingScope scope = recorder.StartAtomicScope("Group"))
        //    {

        //        // We have to *first* add the group to the document to make it recordable.
        //        DesignSurface.CanvasDocument.Children.Add(group);

        //        items.Reparent(group);

               
        //        scope.Complete();
        //    }
        //}
    }
}