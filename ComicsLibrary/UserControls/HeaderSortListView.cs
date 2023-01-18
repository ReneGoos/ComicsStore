using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComicsLibrary.UserControls
{
    public class HeaderSortListView : ListView
    {
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void Sort(string sortBy, ListSortDirection direction)
        {
            var dataView =
              CollectionViewSource.GetDefaultView(ItemsSource);

            dataView.SortDescriptions.Clear();
            var sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        protected override void OnInitialized(EventArgs e)
        {
            AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(HeaderSortListViewClickEvent));
            ((INotifyCollectionChanged)ItemsSource).CollectionChanged += HeaderSortListViewCollectionChangedEvent;
            base.OnInitialized(e);
        }

        private void HeaderSortListViewCollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_lastHeaderClicked != null)
            {
                if (_lastHeaderClicked.Tag is string sortBy)
                {
                    Sort(sortBy, _lastDirection);
                }
            }
        }

        void HeaderSortListViewClickEvent(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
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
