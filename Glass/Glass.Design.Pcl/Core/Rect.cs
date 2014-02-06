namespace Glass.Design.Pcl.Core
{
    public struct Rect : IRect
    {
        private double x;
        private double y;
        private double width;
        private double height;
        

        public Rect(Point location, Size size) : this()
        {
            x = location.X;
            y = location.Y;
            width = size.Width;
            height = size.Height;
        }

        public Rect(double left, double top, double width, double height)
        {
            throw new System.NotImplementedException();
        }

        public double GetCoordinate(CoordinatePart part)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new System.NotImplementedException();
        }

        public double Left { get; private set; }
        public double Top { get; private set; }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public ISize Size
        {
            get { return new Size(width, height); }
        }

        public IPoint Location
        {
            get { return new Point(X, Y); }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Right { get; private set; }
        public double Bottom { get; private set; }

        public bool Equals(Rect other)
        {
            return Left.Equals(other.Left) && Top.Equals(other.Top) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rect && Equals((Rect) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode*397) ^ Top.GetHashCode();
                hashCode = (hashCode*397) ^ Width.GetHashCode();
                hashCode = (hashCode*397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
    }
}