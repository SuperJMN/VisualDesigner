using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Design.Interfaces;
using Glass.Basics.Core;

namespace Glass.Basics.Presentation
{
    public class WrappingAdorner : Adorner {

        private UIElement chrome;
        private ICanvasItem canvasItem;

        public WrappingAdorner(UIElement adornedElement, UIElement chrome, ICanvasItem canvasItem)
            : base(adornedElement)
        {
            Chrome = chrome;
            CanvasItem = canvasItem;
        }

        protected override int VisualChildrenCount {
            get {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index) {
            if (index != 0)
                throw new ArgumentOutOfRangeException();

            return chrome;
        }

        public UIElement Chrome {
            get { return chrome; }
            set {
                if (chrome != null) {
                    RemoveVisualChild(chrome);
                }
                chrome = value;
                if (chrome != null) {
                    AddVisualChild(chrome);
                }
            }
        }

        public ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                if (canvasItem != null)
                {
                    canvasItem.LeftChanged -= CanvasItemOnLeftChanged; 
                    canvasItem.WidthChanged -= CanvasItemOnSizeChanged;
                    canvasItem.HeightChanged-= CanvasItemOnSizeChanged;
                }
                canvasItem = value;

                if (canvasItem != null)
                {
                    canvasItem.LeftChanged += CanvasItemOnLeftChanged;
                    canvasItem.WidthChanged += CanvasItemOnSizeChanged;
                    canvasItem.HeightChanged += CanvasItemOnSizeChanged;
                }
            }
        }

        private void CanvasItemOnSizeChanged(object sender, SizeChangeEventArgs eventArgs)
        {
            InvalidateVisual();
        }

        private void CanvasItemOnLeftChanged(object sender, LocationChangedEventArgs eventArgs)
        {
            InvalidateVisual();
        }

        protected override Size MeasureOverride(Size constraint) {
            chrome.Measure(constraint);
            return chrome.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize) {
            var size = new Size(CanvasItem.Width, CanvasItem.Height);
            chrome.Arrange(new Rect(new Point(CanvasItem.Left, CanvasItem.Top), size));
            return size;
        }
    }
}