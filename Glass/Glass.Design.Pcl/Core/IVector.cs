using System;

namespace Glass.Design.Pcl.Core
{
    public interface IVector
    {
        
        double X { get; set; }

        
        double Y { get; set; }

        
        double Length { get; }

        
        double LengthSquared { get; }

        
        bool Equals(IVector vector1, IVector vector2);

        
        bool Equals(object o);

        
        bool Equals(IVector value);

        
        int GetHashCode();

        
        void Normalize();

        
        double CrossProduct(IVector vector1, IVector vector2);

        
        double AngleBetween(IVector vector1, IVector vector2);

        
        void Negate();                     
                        

        
        IVector Multiply(IVector vector, double scalar);

        
        IVector Multiply(double scalar, IVector vector);

        
        IVector Divide(IVector vector, double scalar);

        
        double Multiply(IVector vector1, IVector vector2);

        
        double Determinant(IVector vector1, IVector vector2);
    }
}