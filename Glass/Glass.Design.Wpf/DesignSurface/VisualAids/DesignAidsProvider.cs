using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Wpf.DesignSurface.VisualAids.Drag;
using Glass.Design.Wpf.DesignSurface.VisualAids.Resize;
using Glass.Design.Wpf.DesignSurface.VisualAids.Selection;
using Glass.Design.Wpf.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Wpf.DesignSurface.VisualAids
{
    internal class DesignAidsProvider
    {

        public DesignAidsProvider(DesignSurface designSurface)
        {
            DesignSurface = designSurface;
            DesignSurface.Loaded += DesignSurfaceOnLoaded;


            SelectionAdorners = new Dictionary<ICanvasItem, SelectionAdorner>();

            EdgeAdorners = new Dictionary<Edge, EdgeAdorner>();

            PlaneOperation = PlaneOperation.Resize;
            DragOperationHost = new DragOperationHost(DesignSurface);
            DragOperationHost.DragStarted += DragOperationHostOnDragStarted;
            DragOperationHost.DragEnd += DragOperationHostOnDragEnd;


            SnappingEngine = new CanvasItemSnappingEngine(4);
            var snappedEdges = SnappingEngine.SnappedEdges;
            ((INotifyCollectionChanged)snappedEdges).CollectionChanged += SnappedEdgesOnCollectionChanged;

            DragOperationHost.SnappingEngine = SnappingEngine;
        }

        private void DragOperationHostOnDragEnd(object sender, EventArgs eventArgs)
        {
            if (ResizingAdorner != null)
            {
                ResizingAdorner.Visibility = Visibility.Visible;                
            }
        }

        private void DragOperationHostOnDragStarted(object sender, EventArgs eventArgs)
        {
            if (ResizingAdorner != null)
            {
                ResizingAdorner.Visibility = Visibility.Hidden;
            }
        }

        public Dictionary<Edge, EdgeAdorner> EdgeAdorners { get; set; }

        private void SnappedEdgesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Edge removedEdge in notifyCollectionChangedEventArgs.OldItems)
                {
                    var adorner = EdgeAdorners[removedEdge];
                    AdornerLayer.Remove(adorner);
                    EdgeAdorners.Remove(removedEdge);
                }
            }
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Edge addedEdge in notifyCollectionChangedEventArgs.NewItems)
                {
                    var edgeAdorner = new EdgeAdorner(DesignSurface, WrappedSelectedItems, addedEdge);
                    EdgeAdorners.Add(addedEdge, edgeAdorner);
                    AdornerLayer.Add(edgeAdorner);
                }
            }
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var adorner in EdgeAdorners.Values)
                {
                    AdornerLayer.Remove(adorner);
                }

                EdgeAdorners.Clear();
            }
        }

        public DragOperationHost DragOperationHost { get; set; }


        private CanvasItemSelection wrappedSelectedItems;

        private CanvasItemSelection WrappedSelectedItems
        {
            get { return wrappedSelectedItems; }
            set
            {
                if (wrappedSelectedItems != null)
                {
                    AdornerLayer.Remove(ResizingAdorner);
                    AdornerLayer.Remove(MovingAdorner);
                }

                wrappedSelectedItems = value;

                if (wrappedSelectedItems != null)
                {
                    var movingControl = new MovingControl();

                    SetupDragOperationHost(movingControl);

                    MovingAdorner = new WrappingAdorner(DesignSurface, movingControl, WrappedSelectedItems);
                    
                    var resizeControl = new ResizeControl(WrappedSelectedItems, DesignSurface, SnappingEngine);
                
                    ResizingAdorner = new WrappingAdorner(DesignSurface, resizeControl, WrappedSelectedItems);
                    AdornerLayer.Add(MovingAdorner);
                    AdornerLayer.Add(ResizingAdorner);                    
                }
            }
        }

        private void SetupDragOperationHost(IInputElement movingControl)
        {
            DragOperationHost.SetDragTarget(movingControl, WrappedSelectedItems);

            var items = DesignSurface.Children;

            var allExceptTarget = items.Except(WrappedSelectedItems.Children);

            DragOperationHost.SnappingEngine.Magnets = allExceptTarget.ToList();
        }


        private Dictionary<ICanvasItem, SelectionAdorner> SelectionAdorners { get; set; }

        private AdornerLayer AdornerLayer { get; set; }

        private DesignSurface DesignSurface
        {
            get;
            set;
        }

        private Adorner ResizingAdorner { get; set; }
        private Adorner MovingAdorner { get; set; }

        private void DesignSurfaceOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AdornerLayer = AdornerLayer.GetAdornerLayer(DesignSurface);
        }

        public void AddItemToSelection(ICanvasItem item)
        {
            AddSelectionAdorner(item);
            WrapSelectedItems();
        }

        public void RemoveItemFromSelection(ICanvasItem item)
        {
            RemoveSelectionAdorner(item);
            WrapSelectedItems();
        }

        private void AddSelectionAdorner(ICanvasItem canvasItem)
        {
            var selectionAdorner = new SelectionAdorner(DesignSurface, canvasItem) { IsHitTestVisible = false };
            AdornerLayer.Add(selectionAdorner);
            SelectionAdorners.Add(canvasItem, selectionAdorner);
        }

        private void WrapSelectedItems()
        {
            var items = SelectionAdorners.Keys.ToList();
            if (items.Any())
            {
                WrappedSelectedItems = new CanvasItemSelectionINPC(items);
            }
            else
            {
                WrappedSelectedItems = null;
            }
        }

        private void RemoveSelectionAdorner(ICanvasItem container)
        {
            var adorner = SelectionAdorners[container];
            SelectionAdorners.Remove(container);
            AdornerLayer.Remove(adorner);
        }

        public PlaneOperation PlaneOperation { get; set; }

        private CanvasItemSnappingEngine SnappingEngine { get; set; }
    }


}