using System.Collections.ObjectModel;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.CanvasItem
{
    public interface ICanvasItem : IPositionable, ISizable
    {
        ObservableCollection<ICanvasItem> Children { get; }
    }
}