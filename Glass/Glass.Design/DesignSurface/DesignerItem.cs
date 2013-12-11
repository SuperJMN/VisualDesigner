using System.Windows;

namespace Glass.Design.DesignSurface
{
    public class DesignerItem : FrameworkElementToCanvasItemAdapter
    {
        static DesignerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }        
    }
}