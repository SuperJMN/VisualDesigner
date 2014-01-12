using System.Collections.Generic;
using System.Linq;

namespace Glass.Design.Pcl.CanvasItem
{
    public static class CanvasItemRelocator
    {
        public static void Reparent(this IList<ICanvasItem> items, ICanvasItem destination)
        {

            var rect = Extensions.GetBoundsFromChildren(items);

            var toRemove = items.ToList();

            destination.SetBounds(rect);

            foreach (var canvasItem in items)
            {
                destination.Children.Add(canvasItem);
                canvasItem.Offset(rect.Location.Negative());
            }

            foreach (var canvasItem in toRemove)
            {
                var parent = canvasItem.Parent;
                parent.Children.Remove(canvasItem);
            }
        }

        public static void RemoveAndPromoteChildren(this ICanvasItem canvasItem)
        {
            var newParent = canvasItem.Parent;

            var children = canvasItem.Children.ToList();

            foreach (var child in children)
            {
                child.Offset(canvasItem.GetLocation());
                canvasItem.Children.Remove(child);
                newParent.Children.Add(child);
            }         
        }
    }
}