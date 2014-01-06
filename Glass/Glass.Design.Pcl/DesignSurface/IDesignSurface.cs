using System.Collections.Generic;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IDesignSurface
    {
        IEnumerable<ICanvasItem> CanvasItems { get; }

        ICommand GroupCommand { get; }

    }
}