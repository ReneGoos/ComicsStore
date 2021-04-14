using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace ComicsLibrary.ViewModels
{
    public class BasicTableViewModel<TService, TIn, TPatch, TOut, TSearch, TEdit> : BasicViewModel, IBasicTableViewModel<TEdit> where TService : IComicsStoreService<TIn, TPatch, TOut, TSearch>
        where TIn : BasicInputModel
        where TPatch : BasicInputModel
        where TOut : BasicOutputModel
        where TSearch : BasicSearchModel, new()
        where TEdit : TableEditModel, new()
    {
        protected readonly TService _itemService;
        protected ICollection<TOut> _items;
        private TEdit _item;

        private CollectionViewSource _itemsViewSource;
        private string _itemFilter;

        public BasicTableViewModel(TService service, IMapper mapper) : base(mapper)
        {
            _itemService = service;

            GetCommand = new RelayCommand<int>(new Action<int>(GetItemAsync));
            NewCommand = new RelayCommand(new Action(NewItem));
            SaveCommand = new RelayCommand(new Action(SaveAsync));
            CancelSaveCommand = new RelayCommand(new Action(CancelSaveAsync));

            NewItem();
        }

        protected virtual void FilterResults(object sender, FilterEventArgs e)
        {
            if (_itemFilter is null || _itemFilter.Length == 0)
            {
                e.Accepted = true;
                return;
            }

            if (e.Item is BasicOutputModel item)
            {
                e.Accepted = item.Name.Contains(_itemFilter, System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

        protected virtual async void GetItemsAsync()
        {
            _items = await _itemService.GetAsync(new TSearch { });
            _itemsViewSource = new CollectionViewSource
            {
                Source = _items
            };
            _itemsViewSource.Filter += FilterResults;
            _itemsViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        protected virtual void CancelSaveAsync()
        {
            if (Item.Id is null)
            {
                NewItem();
            }
            else
            {
                GetItemAsync(Item.Id.Value);
            }
        }

        protected virtual async void SaveAsync()
        {
            var itemInput = Mapper.Map<TIn>(Item);
            var itemOut = (Item.Id is null) ? await _itemService.AddAsync(itemInput) : await _itemService.UpdateAsync(Item.Id.Value, itemInput);

            Items = null;

            Item = Mapper.Map<TEdit>(itemOut);
            Item.PropertyChanged += Item_PropertyChanged;
            IsDirty = false;
        }

        protected virtual void NewItem()
        {
            Item = new TEdit();
            Item.PropertyChanged += Item_PropertyChanged;
            IsDirty = false;
        }

        protected virtual async void GetItemAsync(int id)
        {
            Item = Mapper.Map<TEdit>(await _itemService.GetAsync(id));
            Item.PropertyChanged += Item_PropertyChanged;
            IsDirty = false;
            //return Item is not null;
        }

        protected virtual void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Id":
                    if (Item.Id.HasValue)
                    {
                        GetItemAsync(Item.Id.Value);
                    }
                    else
                    {
                        NewItem();
                    }

                    break;
                case "Name":
                    GetItemsAsync();
                    goto default;
                default:
                    IsDirty = true;
                    break;
            }
        }

        public string ItemFilter
        {
            get
            {
                return _itemFilter;
            }
            set
            {
                Set(ref _itemFilter, value);
                _itemsViewSource.View.Refresh();
            }
        }

        public ICollectionView Items
        {
            get
            {
                if (_itemsViewSource is null)
                {
                    GetItemsAsync();
                }

                return _itemsViewSource.View;
            }
            private set
            {
                if (value is null)
                {
                    GetItemsAsync();
                    ItemFilter = "";
                }

                RaisePropertyChanged("Items");
            }
        }

        public TEdit Item
        {
            get => _item;
            private set
            {
                Set(ref _item, value);
            }
        }
    }
}
