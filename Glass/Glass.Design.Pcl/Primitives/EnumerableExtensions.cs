using System.Collections.Generic;

namespace Glass.Design.Pcl.Primitives
{
    public static class EnumerableExtensions
    {
        public static void SynchronizeListTo<T>(this ICollection<T> current, ICollection<T> toAchieve)
        {
            var toAdd = new List<T>();
            var toRemove = new List<T>();


            foreach (var itemInCurrent in current)
            {
                if (!toAchieve.Contains(itemInCurrent))
                {
                    toRemove.Add(itemInCurrent);
                }
            }

            foreach (var snappedEdge in toAchieve)
            {
                if (!current.Contains(snappedEdge))
                {
                    toAdd.Add(snappedEdge);
                }
            }

            foreach (var itemToRemove in toRemove)
            {
                current.Remove(itemToRemove);
            }

            foreach (var itemToAdd in toAdd)
            {
                current.Add(itemToAdd);
            }
        } 
    }
}