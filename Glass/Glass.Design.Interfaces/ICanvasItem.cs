using System.Collections.ObjectModel;

namespace Design.Interfaces
{
    public interface ICanvasItem : IPositionable, ISizable
    {
        ObservableCollection<ICanvasItem> Children { get; }
    }
}