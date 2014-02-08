namespace Glass.Design.Pcl.Core
{
    public struct Size : ISize
    {
        public Size(double width, double height) : this()
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }


    }
}