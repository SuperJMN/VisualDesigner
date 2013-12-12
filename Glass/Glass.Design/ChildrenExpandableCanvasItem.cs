using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Documents;
using Design.Interfaces;

namespace Glass.Design
{
    public class ChildrenExpandableCanvasItem : CanvasItem
    {
        public ChildrenExpandableCanvasItem(IEnumerable<ICanvasItem> children)
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

            this.LeftChanged += OnLeftChanged;
            this.TopChanged += OnTopChanged;
            this.WidthChanged += OnWidthChanged;            
            this.HeightChanged += OnHeightChanged;
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

        protected void SetLeft(double oldLeft, double newLeft)
        {            
            var delta = newLeft - oldLeft;
            foreach (var canvasItem in Children)
            {
                canvasItem.Left += delta;
            }
        }

        protected void SetWidth(SizeChangeEventArgs sizeChangeEventArgs, double currentParentLeft)
        {
            foreach (var canvasItem in Children)
            {
                Resize(canvasItem, sizeChangeEventArgs, currentParentLeft);
            }
        }

        private void Resize(ICanvasItem canvasItem, SizeChangeEventArgs sizeChange, double currentParentLeft)
        {
            var currentLeft = canvasItem.Left - currentParentLeft;
            var currentWidth = canvasItem.Width;

            var leftProportion = currentLeft / sizeChange.OldValue;
            var widthProportion = currentWidth / sizeChange.OldValue;

            canvasItem.Left = currentParentLeft + sizeChange.NewValue * leftProportion;
            canvasItem.Width = sizeChange.NewValue * widthProportion;
        }

        private class SizeChange
        {
            private readonly double newSize;
            private readonly double currentSize;

            public SizeChange(double currentSize, double newSize)
            {
                this.newSize = newSize;
                this.currentSize = currentSize;
            }

            public double NewSize
            {
                get { return newSize; }
            }

            public double CurrentSize
            {
                get { return currentSize; }
            }
        }

    }
}