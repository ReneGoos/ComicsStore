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
            ICollectionView cvCollectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            if (cvCollectionView == null)
                return;

            // filtrer ... exemple pour tests DI-2015-05105-0
            cvCollectionView.Filter = p_oObject => { return true; /* use your own filter */ };

            // page configuration
            int iMaxItemPerPage = MaxItems;
            int iCurrentPage = Page;
            int iStartIndex = iCurrentPage * iMaxItemPerPage;

            // déterminer les objects "de la page"
            int iCurrentIndex = 0;
            HashSet<object> hsObjectsInPage = new HashSet<object>();
            foreach (object oObject in cvCollectionView)
            {
                // break if MaxItemCount is reached
                if (hsObjectsInPage.Count > iMaxItemPerPage)
                    break;

                // add if StartIndex is reached
                if (iCurrentIndex >= iStartIndex)
                    hsObjectsInPage.Add(oObject);

                // increment
                iCurrentIndex++;
            }

            // refilter
            cvCollectionView.Filter = p_oObject =>
            {
                return cvCollectionView.Contains(p_oObject) && hsObjectsInPage.Contains(p_oObject);
            };
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
                var dataView = CollectionViewSource.GetDefaultView(ItemsSource);
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
