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
            //This will get called when the property of an object inside the collection changes - note you must make it a 'reset' - I don't know, why
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(args);
        }
    }
}
