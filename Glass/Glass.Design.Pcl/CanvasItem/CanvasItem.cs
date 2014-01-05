using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItem : ICanvasItem
    {
        private double left = double.NaN;
        private double top = double.NaN;
        private double width = double.NaN;
        private double height = double.NaN;

        public CanvasItem()
        {
            Children = new CanvasItemCollection();
        }

        public double Right { get { return Left + Width; } }
        public double Bottom { get { return Top + Height; } }
        public CanvasItemCollection Children { get; private set; }

        public double Left
        {
            get { return left; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (value == left)
                {
                    return;
                }

                var oldValue = !double.IsNaN(left) ? left : value;



                var newValue = value;
                left = newValue;


                var locationChangedEventArgs = new LocationChangedEventArgs(oldValue, newValue);
                OnLeftChanged(locationChangedEventArgs);
                RaiseLeftChanged(locationChangedEventArgs);
            }
        }

        public double Top
        {
            get { return top; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (value == top)
                {
                    return;
                }

                var oldValue = !double.IsNaN(top) ? top : value;

                var newValue = value;
                top = newValue;

                var locationChangedEventArgs = new LocationChangedEventArgs(oldValue, newValue);
                OnTopChanged(locationChangedEventArgs);

                RaiseTopChanged(locationChangedEventArgs);
            }
        }

        protected virtual void OnTopChanged(LocationChangedEventArgs locationChangedEventArgs)
        {

        }

        protected virtual void OnLeftChanged(LocationChangedEventArgs locationChangedEventArgs)
        {

        }

        public event EventHandler<LocationChangedEventArgs> LeftChanged;
        public event EventHandler<LocationChangedEventArgs> TopChanged;

        protected virtual void RaiseTopChanged(LocationChangedEventArgs e)
        {
            var handler = TopChanged;
            if (handler != null) handler(this, e);
        }

        protected virtual void RaiseLeftChanged(LocationChangedEventArgs e)
        {
            var handler = LeftChanged;
            if (handler != null) handler(this, e);
        }

        public double Width
        {
            get { return width; }
            set
            {
                var oldValue = !double.IsNaN(width) ? width : value;
                var newValue = Math.Max(value, 0);

                width = value;

                var sizeChangeEventArgs = new SizeChangeEventArgs(oldValue, newValue);
                OnWidthChanged(sizeChangeEventArgs);
                RaiseWidthChanged(sizeChangeEventArgs);
            }
        }

        protected virtual void OnWidthChanged(SizeChangeEventArgs sizeChangeEventArgs)
        {
            foreach (var child in Children)
            {
                ResizeChildWidthProportionally(child, sizeChangeEventArgs);
            }
        }

        private void ResizeChildWidthProportionally(ICanvasItem child, SizeChangeEventArgs sizeChangeEventArgs)
        {
            var widthProp = child.Width / sizeChangeEventArgs.OldValue;
            child.Width = child.Width = widthProp * sizeChangeEventArgs.NewValue;

            var leftProp = child.Left / sizeChangeEventArgs.OldValue;
            child.Left = leftProp * sizeChangeEventArgs.NewValue;
        }

        public double Height
        {
            get { return height; }
            set
            {
                var oldValue = !double.IsNaN(height) ? height : value;
                var newValue = Math.Max(value, 0);

                height = value;

                var sizeChangeEventArgs = new SizeChangeEventArgs(oldValue, newValue);

                OnHeightChanged(sizeChangeEventArgs);
                RaiseHeightChanged(sizeChangeEventArgs);
            }
        }

        protected virtual void OnHeightChanged(SizeChangeEventArgs sizeChangeEventArgs)
        {
            foreach (var child in Children)
            {
                child.SwapCoordinates();
                ResizeChildWidthProportionally(child, sizeChangeEventArgs);
                child.SwapCoordinates();
            }
        }


        public event EventHandler<SizeChangeEventArgs> HeightChanged;

        private void RaiseHeightChanged(SizeChangeEventArgs e)
        {
            var handler = HeightChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<SizeChangeEventArgs> WidthChanged;

        private void RaiseWidthChanged(SizeChangeEventArgs e)
        {
            var handler = WidthChanged;
            if (handler != null) handler(this, e);
        }
    }
}