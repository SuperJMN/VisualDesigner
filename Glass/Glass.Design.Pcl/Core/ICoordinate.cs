namespace Glass.Design.Pcl.Core
{
    public interface ICoordinate
    {
        double GetCoordinate(CoordinatePart part);
        void SetCoordinate(CoordinatePart part, double value);
    }
}