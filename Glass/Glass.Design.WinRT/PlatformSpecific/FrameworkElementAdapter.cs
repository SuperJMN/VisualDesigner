using Windows.UI.Xaml;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class FrameworkElementAdapter : UIElementAdapter, IFrameworkElement
    {
        protected FrameworkElementAdapter(UIElement uiElement) : base(uiElement)
        {
        }

        public double Width
        {
            get { return ((FrameworkElement)UIElement).Width; }
            set { ((FrameworkElement)UIElement).Width = value; }
        }

        public double Height
        {
            get { return ((FrameworkElement)UIElement).Height; }
            set { ((FrameworkElement)UIElement).Height = value; }
        }
    }
}