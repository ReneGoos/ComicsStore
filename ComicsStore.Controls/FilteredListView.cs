using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComicsStore.Controls
{

    public class FilteredListView : ListView
    {
        private CancellationTokenSource src = new();

        static FilteredListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilteredListView), new FrameworkPropertyMetadata(typeof(FilteredListView)));
        }

        public Func<object, string, bool> FilterPredicate
        {
            get { return (Func<object, string, bool>)GetValue(FilterPredicateProperty); }
            set { SetValue(FilterPredicateProperty, value); }
        }

        public static readonly DependencyProperty FilterPredicateProperty =
            DependencyProperty.Register("FilterPredicate", typeof(Func<object, string, bool>), typeof(FilteredListView), new PropertyMetadata(null));

        //public Subject<bool> FilterInputSubject = new Subject<bool>();

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText",
                typeof(string),
                typeof(FilteredListView),
                new PropertyMetadata("", async (d, e) => await (d as FilteredListView).OnChangeTask(e)));
        //This is the 'PropertyChanged' callback that's called whenever the Filter input text is changed


        private async Task OnChangeTask(DependencyPropertyChangedEventArgs args)
        {
            src.Cancel();
            src.Dispose();

            try
            {
                src = new();
                await Task.Delay(700, src.Token).ContinueWith(DoSomeWork, src.Token);
            }
            catch (TaskCanceledException) { }
        }

        public FilteredListView()
        {
            SetDefaultFilterPredicate();
        }

        private void SetDefaultFilterPredicate()
        {
            FilterPredicate = (obj, text) => obj.ToString().ToLower().Contains(text);
        }

        private void DoSomeWork(Task obj)
        {
            var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            if (collectionView == null) return;

            this.Dispatcher.Invoke(() =>
            {
                collectionView.Filter = (item) => FilterPredicate(item, FilterText);

                if (collectionView.Cast<object>().Count() == 1)
                {
                    SelectedItem = collectionView.Cast<object>().First();
                }
            });
        }

        //private void InitThrottle()
        //{
        //    FilterInputSubject.Throttle(TimeSpan.FromMilliseconds(500))
        //        .ObserveOnDispatcher()
        //        .Subscribe(HandleFilterThrottle);
        //}

        //private void HandleFilterThrottle(bool b)
        //{
        //    ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.ItemsSource);
        //    if (collectionView == null) return;
        //    collectionView.Filter = (item) => FilterPredicate(item, FilterText);

        //    if (collectionView.Cast<object>().Count() == 1)
        //    {
        //        SelectedItem = collectionView.Cast<object>().First();
        //    }
        //}
    }
}
