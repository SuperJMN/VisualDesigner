using Model;

namespace ComicDesigner
{
    public interface IEditingContext
    {
        Document Document { get; set; }
    }
}