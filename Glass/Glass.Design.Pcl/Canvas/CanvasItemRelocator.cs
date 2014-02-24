using System.Collections.Generic;
using System.Linq;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.Canvas
{
    public static class CanvasItemRelocator
    {
        public static void Reparent(this IEnumerable<ICanvasItem> items, ICanvasItem destination)
        {

            var rect = Extensions.GetBoundsFromChildren(items);

            var toRemove = items.ToList();

            destination.SetBounds(rect);


            foreach (var canvasItem in toRemove)
            {
                var parent = canvasItem.Parent;
                parent.Children.Remove(canvasItem);
            }

            foreach (var canvasItem in items)
            {
                destination.Children.Add(canvasItem);
                canvasItem.Offset(rect.Location.Negative());
            }

        }

        public static void RemoveAndPromoteChildren(this ICanvasItem canvasItem)
        {
            var newParent = canvasItem.Parent;

            var children = canvasItem.Children.ToList();
            IPoint location = canvasItem.GetPosition();

            foreach (var child in children)
            {
                child.Offset(location);
                canvasItem.Children.Remove(child);
                newParent.Children.Add(child);
            }         
        }
    }
}