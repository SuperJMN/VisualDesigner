using Model;

namespace ComicDesigner
{
    public interface IEditingContext
    {
        Document Document { get; set; }
        double SurfaceWidth { get; set; }
        double SurfaceHeight { get; set; }
    }
}