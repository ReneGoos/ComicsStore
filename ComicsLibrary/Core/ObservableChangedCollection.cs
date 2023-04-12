using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ComicsLibrary.Core
{
    public class ObservableChangedCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        // this collection also reacts to changes in its components' properties
        public ObservableChangedCollection() : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(ObservableChangedCollection_CollectionChanged);
        }

        public ObservableChangedCollection(IEnumerable<T> collection) : base(collection)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(ObservableChangedCollection_CollectionChanged);
        }

        private void ObservableChangedCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, sender);
            OnCollectionChanged(args);
        }
    }
}
