using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.DesignSurface.VisualAids.Drag;
using Glass.Design.Wpf.DesignSurface.VisualAids.Resize;
using Glass.Design.Wpf.DesignSurface.VisualAids.Selection;

namespace Glass.Design.Wpf.DesignSurface.VisualAids
{
    internal class DesignAidsProvider
    {

        public DesignAidsProvider(DesignSurface visual)
        {
            Visual = visual;
            Visual.Loaded += VisualOnLoaded;
     

            SelectionAdorners = new Dictionary<ICanvasItem, SelectionAdorner>();
            DesignOperation = DesignOperation.Resize;
            DragOperationHost = new DragOperationHost(Visual);
            DragOperationHost.SnappingEngine = new CanvasItemSnappingEngine();
        }

        public DragOperationHost DragOperationHost { get; set; }


       private CanvasItemGroup groupedItems;

        private CanvasItemGroup GroupedItems
        {
            get { return groupedItems; }
            set
            {
                if (groupedItems != null)
                {
                    AdornerLayer.Remove(ResizingAdorner);
                    AdornerLayer.Remove(MovingAdorner);
                }

                groupedItems = value;

                if (groupedItems != null)
                {
                    var movingControl = new MovingControl();
                    
                    SetupDragOperationHost(movingControl);

                    MovingAdorner = new WrappingAdorner(Visual, movingControl, GroupedItems);
                    ResizingAdorner = new WrappingAdorner(Visual, new SizingControl { CanvasItem = GroupedItems }, GroupedItems);
                    AdornerLayer.Add(ResizingAdorner);
                    AdornerLayer.Add(MovingAdorner);
                }
            }
        }

        private void SetupDragOperationHost(MovingControl movingControl)
        {
            DragOperationHost.SetDragTarget(movingControl, GroupedItems);
            var canvasItems = Visual.CanvasItems.Where(item => !GroupedItems.Children.Contains(item)).ToList();
            DragOperationHost.SnappingEngine.Magnets = canvasItems;
        }


        private Dictionary<ICanvasItem, SelectionAdorner> SelectionAdorners { get; set; }

        private AdornerLayer AdornerLayer { get; set; }

        private DesignSurface Visual
        {
            get;
            set;
        }

        private Adorner ResizingAdorner { get; set; }
        private Adorner MovingAdorner { get; set; }

        private void VisualOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AdornerLayer = AdornerLayer.GetAdornerLayer(Visual);
        }

        public void AddItemToSelection(ICanvasItem item)
        {
            AddSelectionAdorner(item);
            UpdateGroup();
        }

        public void RemoveItemFromSelection(ICanvasItem item)
        {
            RemoveSelectionAdorner(item);
            UpdateGroup();
        }

        private void AddSelectionAdorner(ICanvasItem canvasItem)
        {
            var selectionAdorner = new SelectionAdorner(Visual, canvasItem) { IsHitTestVisible = false };
            AdornerLayer.Add(selectionAdorner);
            SelectionAdorners.Add(canvasItem, selectionAdorner);
        }

        private void UpdateGroup()
        {
            var items = SelectionAdorners.Keys.ToList();
            if (items.Any())
            {
                GroupedItems = new CanvasItemGroupINPC(items);
            }
            else
            {
                GroupedItems = null;
            }
        }

        private void RemoveSelectionAdorner(ICanvasItem container)
        {
            var adorner = SelectionAdorners[container];
            SelectionAdorners.Remove(container);
            AdornerLayer.Remove(adorner);
        }

        public DesignOperation DesignOperation { get; set; }
    }

    
}