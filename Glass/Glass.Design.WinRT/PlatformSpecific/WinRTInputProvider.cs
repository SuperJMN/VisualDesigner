using System;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class WinRTInputProvider : IInputProvider
    {
        public Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver)
        {
            //var mousePositionRelativeTo = Mouse.GetPosition((IInputElement)inputReceiver);
            //var pclPoint = Mapper.Map<Point>(mousePositionRelativeTo);
            //return pclPoint;
            return new Point();
        }
    }

    
}