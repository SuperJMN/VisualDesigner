using Model;

namespace ComicDesigner.Tooling
{
    public interface ITool
    {
        string Name { get; set; }
        CanvasItemViewModel CreateItem();
        string IconKey { get; set; }
    }

    public abstract class Tool : ITool
    {
        public string Name { get; set; }
        public abstract CanvasItemViewModel CreateItem();

        public string IconKey { get; set; }
    }
}