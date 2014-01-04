using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public class CanvasItem : ICanvasItem
    {
        private double left;
        private double top;
        private double width;
        private double height;

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

                var oldValue = left;
                var newValue = value;
                left = newValue;
                OnLeftChanged(new LocationChangedEventArgs(oldValue, newValue));
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

                var oldValue = top;
                var newValue = value;
                top = newValue;
                OnTopChanged(new LocationChangedEventArgs(oldValue, newValue));
            }
        }

        public event EventHandler<LocationChangedEventArgs> LeftChanged;
        public event EventHandler<LocationChangedEventArgs> TopChanged;

        protected virtual void OnTopChanged(LocationChangedEventArgs e)
        {
            var handler = TopChanged;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnLeftChanged(LocationChangedEventArgs e)
        {
            var handler = LeftChanged;
            if (handler != null) handler(this, e);
        }

        public double Width
        {
            get { return width; }
            set
            {
                var oldValue = width;
                var newValue = Math.Max(value, 0);

                width = value;

                OnWidthChanged(new SizeChangeEventArgs(oldValue, newValue));
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                var oldValue = height;
                var newValue = Math.Max(value, 0);

                height = value;

                OnHeightChanged(new SizeChangeEventArgs(oldValue, newValue));
            }
        }


        public event EventHandler<SizeChangeEventArgs> HeightChanged;

        protected virtual void OnHeightChanged(SizeChangeEventArgs e)
        {
            var handler = HeightChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<SizeChangeEventArgs> WidthChanged;

        protected virtual void OnWidthChanged(SizeChangeEventArgs e)
        {
            var handler = WidthChanged;
            if (handler != null) handler(this, e);
        }
    }
}