using System.Windows;
using Design.Interfaces;
using Glass.Design.CanvasItem;

namespace Glass.Design.DesignSurface.VisualAids
{
    public class DragOperation
    {
        private ICanvasItem Child { get; set; }
        private Point StartingPoint { get; set; }

        public DragOperation(ICanvasItem child, Point startingPoint)
        {
            Child = child;

            StartingPoint = startingPoint;
            ChildStartingPoint = child.GetLocation();
        }

        public Point ChildStartingPoint { get; set; }

        public void NotifyNewPosition(Point newPoint)
        {
            var delta = newPoint - StartingPoint;
            var newChildLocation = ChildStartingPoint + delta;

            var resultingLocation = SnapToGrid(newChildLocation, new Size(24, 24), new Vector(0.6, 0.6));

            Child.SetLocation(resultingLocation);
        }

        private Point SnapToGrid(Point newChildLocation, Size size, Vector power)
        {
            var scopeX = (power.X * size.Width) /2;
            var scopeY = (power.Y * size.Height) / 2;

            var nearestGridX = MathOperations.NearestMultiple(newChildLocation.X, size.Width);
            var x = MathOperations.Snap(newChildLocation.X, nearestGridX, scopeX);

            var nearestGridY = MathOperations.NearestMultiple(newChildLocation.Y, size.Height);
            var y = MathOperations.Snap(newChildLocation.Y, nearestGridY, scopeY);

            return new Point(x, y);
        }
    }
}
