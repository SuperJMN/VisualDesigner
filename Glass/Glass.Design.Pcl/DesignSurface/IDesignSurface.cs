using System.Collections.Generic;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IDesignSurface : ICanvasItemParent
    {        
        ICommand GroupCommand { get; }

    }
}