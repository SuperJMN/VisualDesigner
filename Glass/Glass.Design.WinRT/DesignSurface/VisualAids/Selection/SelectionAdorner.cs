using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
    public class SelectionAdorner : CanvasItemAdorner
    {
        public SelectionAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {

        }
        
        public override object GetCoreInstance()
        {
            var rectangle = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Red),
                Opacity = 0.5,
                Width = CanvasItem.Width,
                Height = CanvasItem.Height,
            };

            return rectangle;
        }      
    }
}