#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

#endregion

namespace Glass.Basics.Wpf.Collections
{
    public class ObservableCollectionSynchronizer<T>
    {
        private ICollection<T> destination;
        private ObservableCollection<T> source;

        public ObservableCollectionSynchronizer()
        {
        }

        public ObservableCollectionSynchronizer(ObservableCollection<T> source, ICollection<T> destination)
        {
            Source = source;
            Destination = destination;
        }

        public ObservableCollection<T> Source
        {
            get { return source; }
            set
            {
                SubscribeNewAndUnsubscribeOldCollectionChanged(source, value);
                source = value;
                DoInitialSyncWithDestination(Source, Destination);
            }
        }

        public ICollection<T> Destination
        {
            get { return destination; }

            set
            {
                destination = value;
                DoInitialSyncWithDestination(Source, Destination);
            }
        }

        private void DoInitialSyncWithDestination(IEnumerable<T> source, ICollection<T> destination)
        {
            if (destination == null || source == null)
            {
                return;
            }

            destination.Clear();
            foreach (var newColumn in source)
            {
                destination.Add(newColumn);
            }
        }

        private void SubscribeNewAndUnsubscribeOldCollectionChanged(INotifyCollectionChanged oldCollection,
            INotifyCollectionChanged newCollection)
        {
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= SourceOnCollectionChanged;
            }

            if (newCollection != null)
            {
                newCollection.CollectionChanged += SourceOnCollectionChanged;
            }
        }

        private void SourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var sourceCollection = e.Action == NotifyCollectionChangedAction.Add
                ? e.NewItems.Cast<T>().ToList()
                : e.OldItems.Cast<T>().ToList();


            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in sourceCollection)
                {
                    Destination.Add(item);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in sourceCollection)
                {
                    Destination.Remove(item);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Destination.Clear();
            }
        }
    }
}