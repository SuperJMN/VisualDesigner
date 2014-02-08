using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.DesignSurface.VisualAids.Drag;
using Glass.Design.WinRT.DesignSurface.VisualAids.Resize;
using Glass.Design.WinRT.DesignSurface.VisualAids.Selection;
using Glass.Design.WinRT.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.WinRT.DesignSurface.VisualAids
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
                    EdgeAdorner edgeAdorner = null; // new EdgeAdorner(DesignSurface, WrappedSelectedItems, addedEdge);
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

                    SetupDragOperationHost(new UIElementAdapter(movingControl));

                    //MovingAdorner = new WrappingAdorner(DesignSurface, movingControl, WrappedSelectedItems);

                    var resizeControl = new ResizeControl(WrappedSelectedItems, DesignSurface, SnappingEngine);

                    //ResizingAdorner = new WrappingAdorner(DesignSurface, resizeControl, WrappedSelectedItems);
                    AdornerLayer.Add(MovingAdorner);
                    AdornerLayer.Add(ResizingAdorner);
                }
            }
        }

        private void SetupDragOperationHost(Glass.Design.Pcl.PlatformAbstraction.IUserInputReceiver movingControl)
        {
            DragOperationHost.SetDragTarget(movingControl, WrappedSelectedItems);

            var items = DesignSurface.CanvasDocument.Children;

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
            //AdornerLayer = AdornerLayer.GetAdornerLayer(DesignSurface);
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

            if (WrappedSelectedItems != null)
            {
                WrappedSelectedItems.Dispose();
            }

            if (items.Any())
            {
                WrappedSelectedItems = new CanvasItemSelection(items);
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

    internal class UIElementAdapter : IUIElement
    {
        private UIElement UIElement { get; set; }

        public UIElementAdapter(UIElement uiElement)
        {
            this.UIElement = uiElement;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public CanvasItemCollection Children { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public ICanvasItemContainer Parent { get; set; }
        public void AddAdorner(IAdorner adorner)
        {
            throw new NotImplementedException();
        }

        public bool IsVisible { get; set; }
        public bool IsHitTestVisible { get; set; }

        public object GetCoreInstance()
        {
            return UIElement;
        }

        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
        public void CaptureInput()
        {
            throw new NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new NotImplementedException();
        }
    }

    internal class AdornerLayer : Collection<object>
    {
    }
}