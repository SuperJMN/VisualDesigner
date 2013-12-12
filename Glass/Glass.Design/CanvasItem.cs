using System;
using System.Collections.ObjectModel;
using Design.Interfaces;

namespace Glass.Design
{
    public class CanvasItem : ICanvasItem
    {
        private double left;
        private double top;
        private double width;
        private double height;
        private CoercionHandler CoerceTop { get; set; }
        private CoercionHandler CoerceLeft { get; set; }

        public CanvasItem()
        {
            Children = new ObservableCollection<ICanvasItem>();
            CoerceTop = DefaultCoercionHandler();
            CoerceLeft = DefaultCoercionHandler();
        }

        private static CoercionHandler DefaultCoercionHandler()
        {
            return value => value;
        }

        public ObservableCollection<ICanvasItem> Children { get; private set; }

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

                var coercedValue = (double)CoerceLeft(value);

                var oldValue = left;
                var newValue = coercedValue;

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
                var coercedValue = (double)CoerceTop(value);

                var oldValue = top;
                var newValue = coercedValue;

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


        public void SetTopCoercionMethod(CoercionHandler handler)
        {
            CoerceTop = handler;
        }

        public void SetLeftCoercionMethod(CoercionHandler handler)
        {
            CoerceLeft = handler;
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