using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Design.Interfaces;

namespace Glass.Design
{
    public class CanvasItemGroup : Collection<ICanvasItem>, ICanvasItem
    {
        public CanvasItemGroup(IList<ICanvasItem> items)
            : base(items)
        {

        }

        public double Left
        {
            get
            {
                if (!Items.Any())
                {
                    return double.NaN;
                }
                var min = Items.Min(item => item.Left);
                return min;
            }
            set
            {
                SetLeft(Items, value);
                OnLocationChanged();
            }
        }

        private void SetLeft(IEnumerable<ICanvasItem> items, double newLeft)
        {
            var oldLeft = Left;
            var delta = newLeft - oldLeft;
            foreach (var canvasItem in items)
            {
                canvasItem.Left += delta;
            }
        }

        public double Top
        {
            get
            {
                if (!Items.Any())
                {
                    return double.NaN;
                }
                var min = Items.Min(item => item.Top);
                return min;
            }
            set
            {
                Items.SwapCoordinates();
                SetLeft(Items, value);
                Items.SwapCoordinates();
                OnLocationChanged();
            }
        }

        public event EventHandler LocationChanged;

        protected virtual void OnLocationChanged()
        {
            var handler = LocationChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public double Width
        {
            get
            {
                return GetWidth(Items);
            }
            set
            {
                if (value == Width)
                {
                    return;
                }
                SetWidth(Items, Width, value);
                OnWidthChanged();
            }
        }

        public double Height
        {
            get
            {
                Items.SwapCoordinates();
                var height = GetWidth(Items);
                Items.SwapCoordinates();
                return height;
            }
            set
            {
                
                var parentSize = Height;
                if (value == parentSize)
                {
                    return;
                }
                Items.SwapCoordinates();                
                SetWidth(Items, parentSize, value);
                Items.SwapCoordinates();
                OnHeightChanged();
            }
        }

        public event EventHandler HeightChanged;

        protected virtual void OnHeightChanged()
        {
            var handler = HeightChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler WidthChanged;

        protected virtual void OnWidthChanged()
        {
            var handler = WidthChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        private void SetWidth(IEnumerable<ICanvasItem> items, double parentSize, double value)
        {
            foreach (var canvasItem in items)
            {
                Resize(canvasItem, new SizeChange(parentSize, value));
            }
        }

        private void Resize(ICanvasItem canvasItem, SizeChange sizeChange)
        {
            var currentLeft = canvasItem.Left - Left;
            var currentWidth = canvasItem.Width;

            var leftProportion = currentLeft / sizeChange.CurrentSize;
            var widthProportion = currentWidth / sizeChange.CurrentSize;

            canvasItem.Left = Left + sizeChange.NewSize * leftProportion;
            canvasItem.Width = sizeChange.NewSize * widthProportion;
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

        private double GetWidth(IList<ICanvasItem> items)
        {
            if (!items.Any())
            {
                return double.NaN;
            }
            var right = items.Max(item => item.Left + item.Width);
            var width = right - Left;
            return width;
        }

      
    }
}