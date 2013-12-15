using System.Windows;
using System.Windows.Controls;

namespace Glass.Design.Wpf.DesignSurface.VisualAids.Drag
{
    public class MovingControl : Control
    {       
        static MovingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovingControl), new FrameworkPropertyMetadata(typeof(MovingControl)));
        }        
    }
}