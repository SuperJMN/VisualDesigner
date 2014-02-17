using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class ControlAdapter : FrameworkElementAdapter, IControl
    {
        public ControlAdapter(Control control)
            : base(control)
        {

        }

    }
}