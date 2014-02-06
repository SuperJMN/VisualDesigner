namespace Glass.Design.Pcl.Core
{
    public interface IPoint
    {
        
        double X { get; set; }
        
        double Y { get; set; }   
        
        void Offset(double offsetX, double offsetY);              
    }

    public struct Point : IPoint
    {
        public Point(double x, double y)
        {
            
        }

        public double X { get; set; }
        public double Y { get; set; }
       

        public void Offset(double offsetX, double offsetY)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(Point other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }
    }
}