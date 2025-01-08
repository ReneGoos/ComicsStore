using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ComicsLibrary.UserControls;

public class HeaderSortListView : ListView
{
    public enum ListSortDirectionSwitch
    {
        Ascending = 0,
        Descending = 1,
        None = 2
    }

    GridViewColumnHeader _lastHeaderClicked = null;
    ListSortDirectionSwitch _lastDirection = ListSortDirectionSwitch.None;

    public int Page
    {
        get 
        { 
            return (int)GetValue(PageProperty); 
        }
        set 
        { 
            SetValue(PageProperty, value); 
        }
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
        get
        { 
            return (int)GetValue(MaxItemsProperty); 
        }
        set
        {
            if (ItemsSource is ICollection collection && value > 0)
            {
                TotalPages = CalculateTotalPages(collection.Count, value);
            }
            SetValue(MaxItemsProperty, value);
        }
    }

    public static readonly DependencyProperty MaxItemsProperty
        = DependencyProperty.Register(
              "MaxItems",
              typeof(int),
              typeof(HeaderSortListView),
              new PropertyMetadata(0, new PropertyChangedCallback(OnMaxItemsChanged))
          );

    public int TotalPages
    {
        get
        {
            return (int)GetValue(TotalPagesProperty);
        }
        set
        {
            SetValue(TotalPagesProperty, value);
        }
    }

    public static readonly DependencyProperty TotalPagesProperty
        = DependencyProperty.Register(
            "TotalPages",
            typeof(int),
            typeof(HeaderSortListView),
            new PropertyMetadata(0, new PropertyChangedCallback(OnTotalPagesChanged))
            );

    private static void OnMaxItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as HeaderSortListView)?.ResetDataView();
    }

    private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as HeaderSortListView)?.ResetDataView();
    }

    private static void OnTotalPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as HeaderSortListView)?.ResetDataView();
    }

    protected int ResetDataView(object _currentObject = null)
    {
        if (MaxItems > 0)
        {
            var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            if (collectionView == null)
                return 1;
        
            // use your own filter 
            collectionView.Filter = o => { return true; };

            // page configuration
            int maxItemPerPage = MaxItems;
            int currentPage = (_currentObject is not null || Page == 0) ? 0 : Page - 1;
            int startIndex = currentPage * maxItemPerPage;
            int newPage = _currentObject is not null ? -1 : currentPage;

            // get the objects "on the page"
            int currentIndex = 0;
            var objectsInPage = new HashSet<object>();
            foreach (var o in collectionView)
            {
                // break if MaxItemCount is reached
                if (objectsInPage.Count >= maxItemPerPage)
                {
                    if (newPage == -1)
                    {
                        objectsInPage.Clear();
                        currentPage++;
                        startIndex = currentPage * maxItemPerPage;
                    }
                    else
                    {
                        break;
                    }
                }

                // add if StartIndex is reached
                if (currentIndex >= startIndex)
                    objectsInPage.Add(o);

                if (o == _currentObject)
                    newPage = currentPage;

                // increment
                currentIndex++;
            }

            // refilter
            collectionView.Filter = o =>
            {
                return collectionView.Contains(o) && objectsInPage.Contains(o);
            };

            return newPage;
        }

        return 1;
    }

    private void Sort(string sortBy, ListSortDirectionSwitch direction)
    {
        var dataView = CollectionViewSource.GetDefaultView(ItemsSource);

        dataView.SortDescriptions.Clear();

        if (direction != ListSortDirectionSwitch.None)
        {
            var sd = new SortDescription(sortBy, (ListSortDirection)direction);
            dataView.SortDescriptions.Add(sd);
        }

        dataView.Refresh();
    }

    private static int CalculateTotalPages(int count, int size)
    {
        if (size > 0 && count > 0)
        {
            return ((count - 1) / size) + 1;
        }
        return 1;
    }

    protected override void OnInitialized(EventArgs e)
    {
        AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(HeaderSortListViewClickEvent));
        base.OnInitialized(e);
    }

    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);
        ((INotifyCollectionChanged)ItemsSource).CollectionChanged += new NotifyCollectionChangedEventHandler(SourceCollectionChanged);

        if (ItemsSource is ICollection collection && MaxItems > 0)
        {
            TotalPages = CalculateTotalPages(collection.Count, MaxItems);
            Page = (TotalPages > 0) ? 1 : 0;
        }

        if (_lastHeaderClicked != null && _lastHeaderClicked.Tag is string sortBy)
        {
            Sort(sortBy, _lastDirection);
            ResetDataView();
        }
        else
        {
            ResetDataView();
        }
    }

    public void SourceCollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
    {
        if (ItemsSource is ICollection collection)
        {
            object currentObject = null;

            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                currentObject = e.NewItems[0];

                if (_lastHeaderClicked != null)
                {
                    Sort(_lastHeaderClicked.Tag as string, _lastDirection);
                }
            }

            TotalPages = CalculateTotalPages(collection.Count, MaxItems);
            int page = ResetDataView(currentObject);

            if (currentObject != null) 
            {
                Page = page + 1;
            }

        }
    }

    void HeaderSortListViewClickEvent(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is GridViewColumnHeader headerClicked)
        {
            if (headerClicked.Tag is string sortBy)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        // Remove arrow from previously sorted header
                        if (_lastHeaderClicked != null)
                        {
                            _lastHeaderClicked.Column.HeaderTemplate = null;
                        }

                        _lastDirection = ListSortDirectionSwitch.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirectionSwitch.None)
                        {
                            _lastDirection = ListSortDirectionSwitch.Ascending;
                        }
                        else
                        {
                            _lastDirection = _lastDirection + 1;
                        }
                    }

                    //var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    //var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;
                    Sort(sortBy, _lastDirection);
                    ResetDataView();

                    switch (_lastDirection)
                    {
                        case ListSortDirectionSwitch.Ascending:
                            headerClicked.Column.HeaderTemplate = TryFindResource("HeaderTemplateArrowUp") as DataTemplate;
                            break;
                        case ListSortDirectionSwitch.Descending:
                            headerClicked.Column.HeaderTemplate = TryFindResource("HeaderTemplateArrowDown") as DataTemplate;
                            break;
                        default:
                            headerClicked.Column.HeaderTemplate = null;
                            break;
                    }

                    _lastHeaderClicked = headerClicked;
                }
            }
        }
    }
}
