using System.Collections.ObjectModel;
using System.Windows;

namespace Glass.Design
{
    public class PointCollection : Collection<Point>
    {
        public void Offset(double x, double y)
        {
            for (var index = 0; index < Items.Count; index++)
            {
                var point = Items[index];
                point.Offset(x, y);
                Items[index] = point;
            }
        }
    }
}