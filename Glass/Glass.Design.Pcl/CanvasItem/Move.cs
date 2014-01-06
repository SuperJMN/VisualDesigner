using System.Collections.Generic;
using System.Linq;

namespace Glass.Design.Pcl.CanvasItem
{
    public class Mover
    {
        public static void Move(IEnumerable<ICanvasItem> items, ICanvasItem destination)
        {
            var left = items.Min(item => item.Left);
            var top = items.Min(item => item.Top);

            destination.Left = left;
            destination.Top = top;
            destination.Width = 200;
            destination.Height = 100;

            foreach (var canvasItem in items)
            {
                destination.Children.Add(canvasItem);
                canvasItem.Left -= destination.Left;
                canvasItem.Top -= destination.Top;
            }
        }         
    }
}