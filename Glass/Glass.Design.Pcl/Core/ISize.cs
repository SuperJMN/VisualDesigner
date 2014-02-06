namespace Glass.Design.Pcl.Core
{
    public interface ISize
    {
        double Width { get; set; }
        double Height { get; set; }       
    }

    public struct Size : ISize
    {
        public double Width { get; set; }
        public double Height { get; set; }


    }
}