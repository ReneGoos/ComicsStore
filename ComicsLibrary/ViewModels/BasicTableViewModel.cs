﻿using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.ViewModels.Interfaces;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using ComicsLibrary.Helpers;
using ComicsLibrary.Navigation;

namespace ComicsLibrary.ViewModels
{
    public class BasicTableViewModel<TService, TIn, TPatch, TOut, TSearch, TEdit> : BasicViewModel, IBasicTableViewModel<TEdit, TOut>
        where TService : IComicsStoreService<TIn, TPatch, TOut, TSearch>
        where TIn : BasicInputModel
        where TPatch : BasicInputModel
        where TOut : BasicOutputModel, new()
        where TSearch : BasicSearch
        where TEdit : TableEditModel, new()
    {
        protected readonly TService _itemService;
        protected ICollection<TOut> _items;
        private TEdit _item;
        private string _queryText;

        private CollectionViewSource _itemsFilteredViewSource;
        private CollectionViewSource _itemsQueryViewSource;

        private void UpdateItemsList(TOut item, bool removeOnly = false)
        {
            var itemFind = _items.FirstOrDefault(i => i.Id == item.Id);
            if (itemFind is not null)
            {
                _ = _items.Remove(itemFind);
            }
            if (!removeOnly)
            {
                _items.Add(item);
            }

            RaisePropertyChanged("Items");

            _itemsFilteredViewSource.View.Refresh();
            _itemsQueryViewSource.View.Refresh();
            RaisePropertyChanged("FilteredItems");
            RaisePropertyChanged("QueryItems");
        }

        public BasicTableViewModel(TService service,
            INavigationService navigationService,
            IMapper mapper) : base(navigationService, mapper)
        {
            _itemService = service;
            GetCommand = new RelayCommand<int>(new Action<int>(GetItemAsync));
            NewCommand = new RelayCommand<bool>(new Action<bool>(NewItem));
            SaveCommand = new RelayCommand(new Action(SaveAsync));
            UndoCommand = new RelayCommand(new Action(CancelSaveAsync));
            DeleteCommand = new RelayCommand<int>(new Action<int>(DeleteAsync));
            ExitCommand = new RelayCommand<bool>(new Action<bool>(ExitAsync));

            NewItem();
        }

        private async void ExitAsync(bool save)
        {
            if (save)
            {
                SaveAsync();
            }

            if (!save || !IsDirty)
            {
                await NavigationService.ClosePageAsync(save, _item.Id);
            }
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

        protected virtual void GetItems()
        {
            var _itemsUnsorted = _itemService.GetAsync().Result;
            _items = _itemsUnsorted.OrderBy(item => item.Name).ToList();

            _itemsFilteredViewSource = new CollectionViewSource
            {
                Source = _items
            };
            _itemsFilteredViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            _itemsQueryViewSource = new CollectionViewSource
            {
                Source = _items
            };
            _itemsQueryViewSource.Filter += QueryResults;
            _itemsQueryViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public bool FilterByString (object item, string text)
        {
            return item is BasicOutputModel basicOutput && basicOutput.Name.Contains(text, StringComparison.InvariantCultureIgnoreCase);
        }

        public Func<object, string, bool> NameFilter => FilterByString;

        protected virtual void CancelSaveAsync()
        {
            if (Item.Id.HasValue)
            {
                GetItemAsync(Item.Id.Value);
            }
            else
            {
                NewItem();
            }
        }

        protected virtual async void SaveAsync()
        {
            if (Item == null)
            {
                return;
            }

            if (!Item.Validate())
            {
                IsDirty = true;
                return;
            }

            var itemInput = Mapper.Map<TIn>(Item);
            var itemOut = Item.Id.HasValue ? await _itemService.UpdateAsync(Item.Id.Value, itemInput) : await _itemService.AddAsync(itemInput);
            UpdateItemsList(itemOut);

            Item = Mapper.Map<TEdit>(itemOut);
            Item.PropertyChanged += ItemPropertyChanged;
            IsDirty = false;
        }

        private static bool ContinueDelete()
        {
            var messageBoxText = "Are you sure you want to delete?";
            var caption = "Delete Item";

            var btnMessageBox = MessageBoxButton.YesNoCancel;
            var icnMessageBox = MessageBoxImage.Warning;

            var rsltMessageBox = MessageBox.Show(messageBoxText, caption, btnMessageBox, icnMessageBox);

            return rsltMessageBox == MessageBoxResult.Yes;
        }

        protected virtual async void DeleteAsync(int id)
        {
            if (ContinueDelete())
            {
                await _itemService.DeleteAsync(id);
                UpdateItemsList(new TOut { Id = id }, true);
            }

            NewItem();
        }

        protected virtual void NewItem(bool bKeep = false)
        {
            var item = new TEdit();

            if (bKeep)
            {
                item = Item.CloneJson();
                //item.ResetId();
            }

            Item = item;
            Item.PropertyChanged += ItemPropertyChanged;
            IsDirty = false;
        }

        protected virtual async void GetItemAsync(int id)
        {
            Item = Mapper.Map<TEdit>(await _itemService.GetAsync(id));

            if (Item == null)
            {
                Item = new TEdit();
            }
            Item.PropertyChanged += ItemPropertyChanged;
            IsDirty = false;
            //return Item is not null;
        }

        protected virtual void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
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
                    GetItems();
                    goto default;
                case "Error":
                    break;
                default:
                    IsDirty = true;
                    break;
            }
        }

        public string QueryText
        {
            get => _queryText;
            set
            {
                Set(ref _queryText, value);
                _itemsQueryViewSource.View.Refresh();
            }
        }

        public ICollectionView FilteredItems
        {
            get
            {
                if (_itemsFilteredViewSource is null)
                {
                    GetItems();
                }

                return _itemsFilteredViewSource.View;
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

        public TEdit Item
        {
            get => _item;
            private set => Set(ref _item, value);
        }
    }
}
