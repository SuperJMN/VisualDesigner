using Windows.UI.Xaml;
using Glass.Design.Pcl.Canvas;
using Glass.Design.WinRT.DesignSurface.VisualAids.Selection;

namespace Glass.Design.WinRT
{
    public class WrappingAdorner : CanvasItemAdorner
    {

        private UIElement chrome;

        public WrappingAdorner(UIElement adornedElement, UIElement chrome, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {
            Chrome = chrome;
        }

        public UIElement Chrome { get; set; }

        //protected override int VisualChildrenCount
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}

        //protected override Visual GetVisualChild(int index)
        //{
        //    if (index != 0)
        //    {
        //        throw new ArgumentOutOfRangeException();
        //    }

        //    return chrome;
        //}

        //public UIElement Chrome
        //{
        //    get { return chrome; }
        //    set
        //    {
        //        if (chrome != null)
        //        {
        //            RemoveVisualChild(chrome);
        //        }
        //        chrome = value;
        //        if (chrome != null)
        //        {
        //            AddVisualChild(chrome);
        //        }
        //    }
        //}

     

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    chrome.Measure(constraint);
        //    return chrome.DesiredSize;
        //}

        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    var size = new Size(CanvasItem.Width, CanvasItem.Height);
        //    chrome.Arrange(new Rect(new Point(CanvasItem.Left, CanvasItem.Top), size));
        //    return size;
        //}
    }
}