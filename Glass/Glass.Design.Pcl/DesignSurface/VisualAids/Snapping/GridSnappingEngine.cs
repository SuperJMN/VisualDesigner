using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Snapping
{
    public class GridSnappingEngine : ISnappingEngine
    {
        public ISize GridSize { get; set; }
        public Vector Power { get; set; }

        public GridSnappingEngine(ISize gridSize, Vector power)
        {
            GridSize = gridSize;
            Power = power;
        }

        public IPoint SnapPoint(IPoint pointToSnap)                   
        {
            var scopeX = (Power.X * GridSize.Width) /2;
            var scopeY = (Power.Y * GridSize.Height) / 2;

            var nearestGridX = MathOperations.NearestMultiple(pointToSnap.X, GridSize.Width);
            var x = MathOperations.Snap(pointToSnap.X, nearestGridX, scopeX);

            var nearestGridY = MathOperations.NearestMultiple(pointToSnap.Y, GridSize.Height);
            var y = MathOperations.Snap(pointToSnap.Y, nearestGridY, scopeY);

            return ServiceLocator.CoreTypesFactory.CreatePoint(x, y);
        
        }
    }
}