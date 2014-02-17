using Glass.Design.Pcl.Canvas;
using Glass.Design.WinRT.DesignSurface.VisualAids.Snapping;
using IUIElement = Glass.Design.Pcl.PlatformAbstraction.IUIElement;

namespace Glass.Design.WinRT
{
    public class WrappingAdorner : CanvasItemAdorner
    {

        
        private IUIElement chrome;

        public WrappingAdorner(IUIElement adornedElement, IUIElement chrome, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {
            Chrome = chrome;
        }

        public IUIElement Chrome
        {
            get { return chrome; }
            set
            {
                chrome = value;
                Chrome.Width = CanvasItem.Width;
                Chrome.Height = CanvasItem.Height;
            }
        }


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

        //public IUIElement Chrome
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
        public override object GetCoreInstance()
        {
            return Chrome;
        }
    }
}