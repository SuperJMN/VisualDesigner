using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Glass.Basics.Styles
{

    public class SystemButton : Button
    {

        static SystemButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SystemButton),  new FrameworkPropertyMetadata(typeof(SystemButton)));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);
            var window = Window.GetWindow(this);
            if (e.ClickCount == 1)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    var positionRelativeToWindow = new Point(VisualOffset.X, VisualOffset.Y);
                    positionRelativeToWindow.Offset(0, ActualHeight);
                    var screenPosition = PointToScreen(positionRelativeToWindow);
                    SystemCommands.ShowSystemMenu(window, screenPosition);
                }

            }
            if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
            {
                Debug.Assert(window != null, "window != null");
                window.Close();
            }
        }
    }
}
