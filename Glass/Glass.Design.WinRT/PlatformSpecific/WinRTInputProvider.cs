using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using AutoMapper;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;
using FoundationPoint = Windows.Foundation.Point;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class WinRTInputProvider : IInputProvider
    {
        public Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver)
        {
            
            var receiver = (UIElement) inputReceiver;
            var position = receiver
                .TransformToVisual(Window.Current.Content)
                .TransformPoint(new FoundationPoint());

            var finalPosition = new FoundationPoint(
                position.X + Window.Current.CoreWindow.PointerPosition.X, 
                position.Y + Window.Current.CoreWindow.PointerPosition.Y
                );

            return Mapper.Map<Point>(finalPosition);
        }
    }

    
}