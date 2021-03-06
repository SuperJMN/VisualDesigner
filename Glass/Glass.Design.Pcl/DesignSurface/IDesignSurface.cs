﻿using System.Windows.Input;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IDesignSurface : IMultiSelector, IUIElement
    {               
        ICommand GroupCommand { get; }
        ICanvasItemContainer CanvasDocument { get; }
    }
}