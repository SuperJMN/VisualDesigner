namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class Edge
    {
        public Edge(double origin, double length)
        {
            Origin = origin;
            Length = length;
        }

        public double Origin { get; set; }
        public double Length { get; set; }
    }
}