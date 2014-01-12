using System.Collections.Generic;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IDesignSurface : ICanvasItemParent, ICanvasSelector
    {               
        ICommand GroupCommand { get; }
    }

    public interface ICanvasSelector
    {
        CanvasItemCollection SelectedCanvasItems { get; }
    }
}