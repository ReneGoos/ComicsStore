using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.ViewModels;
using ComicsLibrary.ViewModels.Interfaces;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using SoftGoosR.Common.Core;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;

namespace ComicsLibrary.ListViewModels;

public class BasicListViewModel<TService, TIn, TPatch, TOut, TSearch> : ObservableObject, IBasicListViewModel<TOut>, INotifyItemChanged
    where TService : IComicsStoreService<TIn, TPatch, TOut, TSearch>
    where TIn : BasicInputModel
    where TPatch : BasicInputModel
    where TOut : BasicOutputModel, new()
    where TSearch : BasicSearch
{
    private readonly TService _itemService;
    protected ICollection<TOut> _items;
    private string _queryText;
    private CollectionViewSource _itemsQueryViewSource;

    public event ItemChangedEventHandler ItemChanged;

    protected void RaiseItemChanged(string itemName, int? id, ActionType actionType)
    {
        ItemChanged?.Invoke(this, new ItemChangedEventArgs(itemName, id, actionType));
    }

    public BasicListViewModel(TService itemService)
    {
        _itemService = itemService;
    }
    public void UpdateItems()
    {
        RaisePropertyChanged("Items");

        //_itemsFilteredViewSource?.View.Refresh();
        _itemsQueryViewSource?.View.Refresh();
        RaisePropertyChanged("FilteredItems");
        RaisePropertyChanged("QueryItems");
    }

    public void UpdateItemsList(TOut item, bool removeOnly = false)
    {
        if (_items == null)
        {
            GetItems();
            if (_items == null)
            {
                return;
            }
        }

        var itemFind = _items.FirstOrDefault(i => i.Id == item.Id);
        if (itemFind is not null)
        {
            _ = _items.Remove(itemFind);
        }
        if (!removeOnly)
        {
            _items.Add(item);
        }
        
        UpdateItems();
    }

    protected virtual void QueryResults(object sender, FilterEventArgs e)
    {
        if (_queryText is null || _queryText.Length == 0)
        {
            e.Accepted = true;
            return;
        }

        if (e.Item is BasicOutputModel item)
        {
            e.Accepted = item.Name.Contains(_queryText, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }

    private void GetItems()
    {
        var _itemsUnsorted = _itemService.GetAsync().Result;
        _items = [.. _itemsUnsorted.OrderBy(item => item.Name)];

        _itemsQueryViewSource = new CollectionViewSource
        {
            Source = _items
        };
        _itemsQueryViewSource.Filter += QueryResults;
        _itemsQueryViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
    }

    private static bool ComplexContains(string source, string value)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf
            (source, value, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        return index != -1;
    }

    public bool FilterByString(object item, string text)
    {
        return item is BasicOutputModel basicOutput && ComplexContains(basicOutput.Name, text);
    }

    public Func<object, string, bool> NameFilter => FilterByString;

    public string QueryText
    {
        get => _queryText;
        set
        {
            Set(ref _queryText, value);
            _itemsQueryViewSource.View.Refresh();
        }
    }

    public ICollectionView QueryItems
    {
        get
        {
            if (_itemsQueryViewSource is null)
            {
                GetItems();
            }

            return _itemsQueryViewSource.View;
        }
    }

    public IEnumerable<TOut> Items
    {
        get
        {
            if (_items is null)
            {
                GetItems();
            }

            return _items; // ViewSource.View;
        }
    }
}
