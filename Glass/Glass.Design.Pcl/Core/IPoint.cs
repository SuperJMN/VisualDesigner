namespace Glass.Design.Pcl.Core
{
    public interface IPoint
    {
        
        double X { get; set; }

        
        double Y { get; set; }

        
        bool Equals(IPoint point1, IPoint point2);

        
        bool Equals(object o);

        
        bool Equals(IPoint value);

        
        int GetHashCode();

        
        void Offset(double offsetX, double offsetY);

        
        IPoint Add(Vector vector);

        
        IPoint Subtract(Vector vector);

        Vector Subtract(IPoint point2);
    }
}