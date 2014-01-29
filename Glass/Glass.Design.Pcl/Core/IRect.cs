using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.Core
{

    public interface ICoordinate
    {
        double GetCoordinate(CoordinatePart part);
        void SetCoordinate(CoordinatePart part, double value);
    }

    public enum CoordinatePart
    {
        None,
        Left,
        Right,
        Top,
        Bottom,
        Width,
        Height

    }
    public interface IRect : ICoordinate
    {
        double Left { get; }
        double Top { get; }
        double Width { get; set; }
        double Height { get; set; }
        ISize Size { get; set; }
        IPoint Location { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Right { get; }
        double Bottom { get; }
    }
}