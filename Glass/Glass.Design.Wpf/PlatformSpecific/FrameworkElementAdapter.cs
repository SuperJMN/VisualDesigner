using System.Windows;
using System.Windows.Controls;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Wpf.PlatformSpecific
{
    public class FrameworkElementAdapter : UIElementAdapter, IFrameworkElement
    {
        public FrameworkElementAdapter(UIElement uiElement) : base(uiElement)
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