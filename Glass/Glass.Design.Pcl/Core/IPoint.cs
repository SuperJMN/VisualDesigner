namespace Glass.Design.Pcl.Core
{
    public interface IPoint
    {
        
        double X { get; set; }
        
        double Y { get; set; }   
        
        void Offset(double offsetX, double offsetY);              
    }
}