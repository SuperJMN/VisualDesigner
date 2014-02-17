using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.Wpf.DesignSurface.VisualAids.Selection;

namespace Glass.Design.Wpf
{
    public class WrappingAdorner : CanvasItemAdorner
    {

        private IControl chrome;
        private UIElement chromeCoreInstance;

        public WrappingAdorner(IUIElement adornedElement, IControl chrome, ICanvasItem canvasItem)
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

        public IControl Chrome
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

                    Chrome.Width = CanvasItem.Width;
                    Chrome.Height = CanvasItem.Height;

                    AddVisualChild(ChromeCoreInstance);
                }
            }
        }

        private Control ChromeCoreInstance
        {
            get { return (Control)Chrome.GetCoreInstance(); }
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