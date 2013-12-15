using System.Windows;

namespace Glass.Design.DesignSurface.VisualAids.Snapping
{
    public class GridSnappingEngine : ISnappingEngine
    {
        public Size GridSize { get; set; }
        public Vector Power { get; set; }

        public GridSnappingEngine(Size gridSize, Vector power)
        {
            GridSize = gridSize;
            Power = power;
        }

        public Point SnapPoint(Point pointToSnap)                   
        {
            var scopeX = (Power.X * GridSize.Width) /2;
            var scopeY = (Power.Y * GridSize.Height) / 2;

            var nearestGridX = MathOperations.NearestMultiple(pointToSnap.X, GridSize.Width);
            var x = MathOperations.Snap(pointToSnap.X, nearestGridX, scopeX);

            var nearestGridY = MathOperations.NearestMultiple(pointToSnap.Y, GridSize.Height);
            var y = MathOperations.Snap(pointToSnap.Y, nearestGridY, scopeY);

            return new Point(x, y);
        
        }
    }
}