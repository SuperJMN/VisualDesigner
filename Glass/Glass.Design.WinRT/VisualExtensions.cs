using Windows.UI.Xaml;
using AutoMapper;
using Glass.Design.Pcl.Core;

using FoundationPoint = Windows.Foundation.Point;

namespace Glass.Design.WinRT
{
    static public class VisualExtensions
    {
        public static Rect GetRectRelativeToParent(this UIElement parent, UIElement child)
        {
            var transform = parent.TransformToVisual(child);
            var point = transform.TransformPoint(new FoundationPoint());
            return new Rect(Mapper.Map<Point>(point), Mapper.Map<Size>(child.RenderSize));
        }

    }
}