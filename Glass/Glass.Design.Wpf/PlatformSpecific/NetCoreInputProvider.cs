using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;
using Point = Glass.Design.Pcl.Core.Point;

namespace Glass.Design.Wpf.PlatformSpecific
{
    public class NetCoreInputProvider : IInputProvider
    {
        public Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver)
        {
            var mousePositionRelativeTo = Mouse.GetPosition((IInputElement) inputReceiver);
            var pclPoint = Mapper.Map<Point>(mousePositionRelativeTo);
            return pclPoint;
        }
    }
}