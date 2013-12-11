using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Glass.Design.Selection
{
    public class LayeredSelection<T>
    {
        private IDictionary<int, IList<T>> LayersDictionary { get; set; }
        private HashSet<T> ItemsInSelection { get; set; }

        public LayeredSelection()
        {
            LayersDictionary = new Dictionary<int, IList<T>>();
            ItemsInSelection = new HashSet<T>();
        }

        public void Add(T item, int layerId)
        {
            var selectionList = GetLayerOrCreateIfNeeded(layerId);

            CombinedAdd(selectionList, item);
        }

        private void CombinedAdd(IList<T> selectionList, T item)
        {
            selectionList.Add(item);
            var existing = ItemsInSelection.Add(item);

            if (!existing)
            {
                throw new InvalidOperationException("The item was already selected");
            }
        }

        private IList<T> GetLayerOrCreateIfNeeded(int layerId)
        {
            IList<T> selectionList;
            if (!LayersDictionary.ContainsKey(layerId))
            {
                selectionList = new List<T>();
                LayersDictionary.Add(layerId, selectionList);
            }
            else
            {
                selectionList = LayersDictionary[layerId];
            }
            return selectionList;
        }

        public bool Contains(T item)
        {
            return ItemsInSelection.Contains(item);
        }

        public IList<T> GetEffectiveSelection()
        {

            var firstLayerKey = LayersDictionary.Keys.First();
            return new ReadOnlyCollection<T>(LayersDictionary[firstLayerKey]);
        }
    }
}