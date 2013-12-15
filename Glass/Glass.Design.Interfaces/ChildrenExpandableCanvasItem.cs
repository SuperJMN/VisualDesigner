using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl
{
    public class ChildrenExpandableCanvasItem : CanvasItem.CanvasItem
    {
        protected ChildrenExpandableCanvasItem(IEnumerable<ICanvasItem> children)
        {
            foreach (var canvasItem in children)
            {
                Children.Add(canvasItem);
            }

            Children.CollectionChanged += ChildrenOnCollectionChanged;

            Left = GetLeftFromChildren(Children);
            Top = GetTopFromChildren(Children);
            Width = GetWidthFromChildren(Children);
            Height = GetHeightFromChildren(Children);

            LeftChanged += OnLeftChanged;
            TopChanged += OnTopChanged;
            WidthChanged += OnWidthChanged;            
            HeightChanged += OnHeightChanged;
        }

        private void OnWidthChanged(object sender, SizeChangeEventArgs sizeChangeEventArgs)
        {
            SetWidth(sizeChangeEventArgs, Left);
        }

        private void OnHeightChanged(object sender, SizeChangeEventArgs sizeChangeEventArgs)
        {
            Children.SwapCoordinates();
            SetWidth(sizeChangeEventArgs, Top);
            Children.SwapCoordinates();
        }

        private void OnTopChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {
            Children.SwapCoordinates();
            SetLeft(locationChangedEventArgs.OldValue, locationChangedEventArgs.NewValue);            
            Children.SwapCoordinates();
        }

        private double GetWidthFromChildren(ObservableCollection<ICanvasItem> children)
        {
            return GetMaxRightFromChildren(children) - Left;
        }

        public ChildrenExpandableCanvasItem() : this(new List<ICanvasItem>())
        {
        }

        private void OnLeftChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {
            SetLeft(locationChangedEventArgs.OldValue, locationChangedEventArgs.NewValue);            
        }

        private double GetTopFromChildren(ObservableCollection<ICanvasItem> children)
        {
            if (!children.Any())
            {
                return double.NaN;
            }
            var min = children.Min(item => item.Top);
            return min;
        }

        private double GetLeftFromChildren(ObservableCollection<ICanvasItem> children)
        {
            if (!children.Any())
            {
                return double.NaN;
            }
            var min = children.Min(item => item.Left);
            return min;
        }

        private double GetHeightFromChildren(IList<ICanvasItem> children)
        {
            children.SwapCoordinates();
            var maxBottom = GetMaxRightFromChildren(children);
            children.SwapCoordinates();
            return maxBottom - Top;
        }

        private double GetMaxRightFromChildren(IList<ICanvasItem> items)
        {
            if (!items.Any())
            {
                return double.NaN;
            }
            var right = items.Max(item => item.Left + item.Width);
            var width = right;
            return width;
        }      

        

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            throw new NotImplementedException();
        }

        private void SetLeft(double oldLeft, double newLeft)
        {            
            var delta = newLeft - oldLeft;
            foreach (var canvasItem in Children)
            {
                canvasItem.Left += delta;
            }
        }

        private void SetWidth(SizeChangeEventArgs sizeChangeEventArgs, double currentParentLeft)
        {
            foreach (var canvasItem in Children)
            {
                Resize(canvasItem, sizeChangeEventArgs, currentParentLeft);
            }
        }

        private static void Resize(ICanvasItem canvasItem, SizeChangeEventArgs sizeChange, double currentParentLeft)
        {
            var currentLeft = canvasItem.Left - currentParentLeft;
            var currentWidth = canvasItem.Width;

            var leftProportion = currentLeft / sizeChange.OldValue;
            var widthProportion = currentWidth / sizeChange.OldValue;

            canvasItem.Left = currentParentLeft + sizeChange.NewValue * leftProportion;
            canvasItem.Width = sizeChange.NewValue * widthProportion;
        }
    }
}