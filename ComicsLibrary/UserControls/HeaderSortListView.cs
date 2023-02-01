using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComicsLibrary.UserControls
{
    public class HeaderSortListView : ListView
    {
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public int Page
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty
            = DependencyProperty.Register(
                  "Page",
                  typeof(int),
                  typeof(HeaderSortListView),
                  new PropertyMetadata(0, new PropertyChangedCallback(OnPageChanged))
              );

        public int MaxItems
        {
            get { return (int)GetValue(MaxItemsProperty); }
            set { SetValue(MaxItemsProperty, value); }
        }

        public static readonly DependencyProperty MaxItemsProperty
            = DependencyProperty.Register(
                  "MaxItems",
                  typeof(int),
                  typeof(HeaderSortListView),
                  new PropertyMetadata(0, new PropertyChangedCallback(OnMaxItemsChanged))
              );

        private static void OnMaxItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as HeaderSortListView)?.ResetDataView();
        }

        private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as HeaderSortListView)?.ResetDataView();
        }

        protected void ResetDataView()
        {
            var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            if (collectionView == null)
                return;
            
            // use your own filter 
            collectionView.Filter = o => { return true; };

            if (MaxItems > 0)
            {
                // page configuration
                int maxItemPerPage = MaxItems;
                int currentPage = Page == 0 ? 0 : Page - 1;
                int startIndex = currentPage * maxItemPerPage;

                // get the objects "on the page"
                int currentIndex = 0;
                var objectsInPage = new HashSet<object>();
                foreach (var o in collectionView)
                {
                    // break if MaxItemCount is reached
                    if (objectsInPage.Count >= maxItemPerPage)
                        break;

                    // add if StartIndex is reached
                    if (currentIndex >= startIndex)
                        objectsInPage.Add(o);

                    // increment
                    currentIndex++;
                }

                // refilter
                collectionView.Filter = o =>
                {
                    return collectionView.Contains(o) && objectsInPage.Contains(o);
                };
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            var dataView = CollectionViewSource.GetDefaultView(ItemsSource);

            dataView.SortDescriptions.Clear();
            var sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
            ResetDataView();
        }

        protected override void OnInitialized(EventArgs e)
        {
            AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(HeaderSortListViewClickEvent));
            base.OnInitialized(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            if (_lastHeaderClicked != null && _lastHeaderClicked.Tag is string sortBy)
            {
                Sort(sortBy, _lastDirection);
            }
            else
            {
                ResetDataView();
            }
        }

        void HeaderSortListViewClickEvent(object sender, RoutedEventArgs e)
        {
            ListSortDirection direction;

            if (e.OriginalSource is GridViewColumnHeader headerClicked)
            {
                if (headerClicked.Tag is string sortBy)
                {
                    if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                    {
                        if (headerClicked != _lastHeaderClicked)
                        {
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            if (_lastDirection == ListSortDirection.Ascending)
                            {
                                direction = ListSortDirection.Descending;
                            }
                            else
                            {
                                direction = ListSortDirection.Ascending;
                            }
                        }

                        //var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                        //var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;
                        Sort(sortBy, direction);

                        if (direction == ListSortDirection.Ascending)
                        {
                            headerClicked.Column.HeaderTemplate =
                              TryFindResource("HeaderTemplateArrowUp") as DataTemplate;
                        }
                        else
                        {
                            headerClicked.Column.HeaderTemplate =
                              TryFindResource("HeaderTemplateArrowDown") as DataTemplate;
                        }

                        // Remove arrow from previously sorted header
                        if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                        {
                            _lastHeaderClicked.Column.HeaderTemplate = null;
                        }

                        _lastHeaderClicked = headerClicked;
                        _lastDirection = direction;
                    }
                }
            }
        }
    }
}
