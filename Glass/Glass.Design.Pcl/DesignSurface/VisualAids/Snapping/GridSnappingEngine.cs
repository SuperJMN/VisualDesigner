using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class GridSnappingEngine : SnappingEngine
    {
        public ISize GridSize { get; set; }
        public IVector Power { get; set; }

        public GridSnappingEngine(ISize gridSize, IVector power)
        {
            GridSize = gridSize;
            Power = power;
        }

        public override double SnapPoint(double pointToSnap)                   
        {
            var scopeX = (Power.X * GridSize.Width) /2;
            
            var nearestGridX = MathOperations.NearestMultiple(pointToSnap, GridSize.Width);
            var x = MathOperations.Snap(pointToSnap, nearestGridX, scopeX);
            
            return x;
        
        }
    }
}