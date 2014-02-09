using System;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT
{
    public class InputDeviceEventHandlerArgs
    {
        public bool Handled { get; set; }

        public Point GetPoint(IUIElement frameOfReference)
        {
            throw new NotImplementedException();
        }
    }
}