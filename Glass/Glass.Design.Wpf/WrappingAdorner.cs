using System;
using System.Windows;
using System.Windows.Media;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.Wpf.DesignSurface.VisualAids.Selection;

namespace Glass.Design.Wpf
{
    public class WrappingAdorner : CanvasItemAdorner
    {

        private IUIElement chrome;
        private UIElement chromeCoreInstance;
        
        public WrappingAdorner(IUIElement adornedElement, IUIElement chrome, ICanvasItem canvasItem)
            : base((UIElement) adornedElement.GetCoreInstance(), canvasItem)
        {
            Chrome = chrome;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (Visual) (chrome.GetCoreInstance());
        }

        public IUIElement Chrome
        {
            get { return chrome; }
            set
            {
                if (chrome != null)
                {
                    RemoveVisualChild(ChromeCoreInstance);
                }
                chrome = value;
                if (chrome != null)
                {
                    AddVisualChild(ChromeCoreInstance);
                }
            }
        }

        private UIElement ChromeCoreInstance
        {
            get { return (UIElement) Chrome.GetCoreInstance(); }
        }


        protected override Size MeasureOverride(Size constraint)
        {
            ChromeCoreInstance.Measure(constraint);
            return ChromeCoreInstance.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = new Size(CanvasItem.Width, CanvasItem.Height);
            ChromeCoreInstance.Arrange(new Rect(new Point(CanvasItem.Left, CanvasItem.Top), size));
            return size;
        }
    }
}