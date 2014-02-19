using Model;

namespace ComicDesigner.Tools
{
    public interface ITool
    {
        string Name { get; set; }
        CanvasItemViewModel CreateItem();
    }
}