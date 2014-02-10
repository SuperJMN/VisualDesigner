using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Glass.Basics.Wpf.Styles
{
    public class CloseButton : ToggleButton
    {
        static CloseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton), new FrameworkPropertyMetadata(typeof(CloseButton)));
        }

        protected override void OnClick()
        {
            var window = Window.GetWindow(this);
            Debug.Assert(window != null, "window != null");
            window.Close();
        }       
    }
}
