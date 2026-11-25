using ComicsLibrary.Core;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using SoftGoosR.Common.Core;
using System.Globalization;

namespace ComicsLibrary.ListViewModels;

public class BasicListViewModel<TService, TIn, TPatch, TOut, TSearch>(TService itemService) : ObservableObject, IBasicListViewModel<TOut>, INotifyItemChanged
    where TService : IComicsStoreService<TIn, TPatch, TOut, TSearch>
    where TIn : BasicInputModel
    where TPatch : BasicInputModel
    where TOut : BasicOutputModel, new()
    where TSearch : BasicSearch
{
    private readonly TService _itemService = itemService;
    protected ICollection<TOut> _items;

    public event ItemChangedEventHandler ItemChanged;

    protected void RaiseItemChanged(string itemName, int? id, ActionType actionType)
    {
        ItemChanged?.Invoke(this, new ItemChangedEventArgs(itemName, id, actionType));
    }

    public void UpdateItems()
    {
        RaisePropertyChanged("Items");
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

    private void GetItems()
    {
        var _itemsUnsorted = _itemService.GetAsync().Result;
        _items = [.. _itemsUnsorted.OrderBy(item => item.Name)];
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
