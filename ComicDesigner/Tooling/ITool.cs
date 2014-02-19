using Model;

namespace ComicDesigner.Tooling
{
    public interface ITool
    {
        string Name { get; set; }
        CanvasItemViewModel CreateItem();
    }
}