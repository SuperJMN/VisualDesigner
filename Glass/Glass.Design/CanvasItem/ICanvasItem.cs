using System.Collections.ObjectModel;
using Glass.Design.DesignSurface;

namespace Glass.Design.CanvasItem
{
    public interface ICanvasItem : IPositionable, ISizable
    {
        ObservableCollection<ICanvasItem> Children { get; }
    }
}