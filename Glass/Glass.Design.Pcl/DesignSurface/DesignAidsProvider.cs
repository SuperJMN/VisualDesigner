using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Drag;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.DesignSurface
{
    public class DesignAidsProvider
    {

        public DesignAidsProvider(IDesignSurface designSurface)
        {
            DesignSurface = designSurface;


            SelectionAdorners = new Dictionary<ICanvasItem, IAdorner>();

            EdgeAdorners = new Dictionary<Edge, IAdorner>();

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
                ResizingAdorner.IsVisible = true;
            }
        }

        private void DragOperationHostOnDragStarted(object sender, EventArgs eventArgs)
        {
            if (ResizingAdorner != null)
            {
                ResizingAdorner.IsVisible = false;
            }
        }

        public Dictionary<Edge, IAdorner> EdgeAdorners { get; set; }

        private void SnappedEdgesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Edge removedEdge in notifyCollectionChangedEventArgs.OldItems)
                {
                    var adorner = EdgeAdorners[removedEdge];
                    DesignSurface.RemoveAdorner(adorner);
                    EdgeAdorners.Remove(removedEdge);
                }
            }
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Edge addedEdge in notifyCollectionChangedEventArgs.NewItems)
                {
                    var edgeAdorner = ServiceLocator.UIElementFactory.CreateEdgeAdorner(DesignSurface, WrappedSelectedItems, addedEdge);
                    EdgeAdorners.Add(addedEdge, edgeAdorner);
                    DesignSurface.AddAdorner(edgeAdorner);
                }
            }
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var adorner in EdgeAdorners.Values)
                {
                    DesignSurface.RemoveAdorner(adorner);
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
                    DesignSurface.RemoveAdorner(ResizingAdorner);
                    DesignSurface.RemoveAdorner(MovingAdorner);
                }

                wrappedSelectedItems = value;

                if (wrappedSelectedItems != null)
                {
                    var movingControl = ServiceLocator.UIElementFactory.CreateMovingControl();

                    SetupDragOperationHost(movingControl);

                    MovingAdorner = ServiceLocator.UIElementFactory.CreateWrappingAdorner(DesignSurface, movingControl, WrappedSelectedItems);

                    var resizeControl = ServiceLocator.UIElementFactory.CreateResizeControl(WrappedSelectedItems, DesignSurface, SnappingEngine);

                    ResizingAdorner = ServiceLocator.UIElementFactory.CreateWrappingAdorner(DesignSurface, resizeControl, WrappedSelectedItems);
                    DesignSurface.AddAdorner(MovingAdorner);
                    DesignSurface.AddAdorner(ResizingAdorner);                    
                }
            }
        }

        private void SetupDragOperationHost(IUserInputReceiver movingControl)
        {
            DragOperationHost.SetDragTarget(movingControl, WrappedSelectedItems);

            var items = DesignSurface.Children;

            var allExceptTarget = items.Except(WrappedSelectedItems.Children);

            DragOperationHost.SnappingEngine.Magnets = allExceptTarget.ToList();
        }


        private Dictionary<ICanvasItem, IAdorner> SelectionAdorners { get; set; }

        private IDesignSurface DesignSurface
        {
            get;
            set;
        }

        private IAdorner ResizingAdorner { get; set; }
        private IAdorner MovingAdorner { get; set; }


        public void AddItemToSelection(ICanvasItem item)
        {
            //AddSelectionAdorner(item);
            WrapSelectedItems();
        }

        public void RemoveItemFromSelection(ICanvasItem item)
        {
            //RemoveSelectionAdorner(item);
            WrapSelectedItems();
        }

        private void AddSelectionAdorner(ICanvasItem canvasItem)
        {
            var selectionAdorner = ServiceLocator.UIElementFactory.CreateSelectionAdorner(DesignSurface, canvasItem);
            DesignSurface.AddAdorner(selectionAdorner);
            SelectionAdorners.Add(canvasItem, selectionAdorner);
        }

        private void WrapSelectedItems()
        {
            if (WrappedSelectedItems != null)
            {
                WrappedSelectedItems.Dispose();
            }

            if (DesignSurface.SelectedItems.Count > 0)
            {
                WrappedSelectedItems = new CanvasItemSelection(DesignSurface.SelectedItems.Cast<ICanvasItem>());
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
            DesignSurface.RemoveAdorner(adorner);
        }

        public PlaneOperation PlaneOperation { get; set; }

        private CanvasItemSnappingEngine SnappingEngine { get; set; }
    }


}