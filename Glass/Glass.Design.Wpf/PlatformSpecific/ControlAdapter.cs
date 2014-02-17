using Glass.Design.Pcl.PlatformAbstraction;
using System.Windows.Controls;

namespace Glass.Design.Wpf.PlatformSpecific
{
    public class ControlAdapter : FrameworkElementAdapter, IControl
    {
        public ControlAdapter(Control control)
            : base(control)
        {

        }     
    }
}