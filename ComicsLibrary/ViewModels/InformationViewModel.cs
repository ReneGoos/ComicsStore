using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.Helpers;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels;

public class InformationViewModel : BasicEditModel
{
    private readonly IInformationService _informationViewService;
    private readonly IMapper _mapper;
    private PagingCollectionView<InformationEditModel> _pagingCollection;

    private IdSearch _search;
    private string _itemSort;

    public ICommand GetCommand { get; protected set; }
    public ICommand StoreInformationWindowCommand { get; protected set; }

    public InformationViewModel(IInformationService informationViewService,
                            IMapper mapper) : base()
    {
        _informationViewService = informationViewService;
        _mapper = mapper;

        StoreInformationWindowCommand = new RelayCommand(new Action(StoreInformationWindow));
        GetCommand = new RelayCommand<IdSearch>(new Action<IdSearch>(GetInformation));
    }

    private async void GetInformation(IdSearch search)
    {
        _search = search;
        var list = _mapper.Map<List<InformationEditModel>>(await _informationViewService.GetAsync(search)); ; ;
        PagingCollection = new PagingCollectionView<InformationEditModel>(list, 50);
    }

    private async void StoreInformationWindow()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            var report = await _informationViewService.GetExportAsync(_search);

            await File.WriteAllTextAsync(saveFileDialog.FileName, report);
        }
    }

    private async void Refresh()
    {
        var list = _mapper.Map<List<InformationEditModel>>(await _informationViewService.GetAsync(_search)); ; ;
        PagingCollection = new PagingCollectionView<InformationEditModel>(list, 50);
    }

    public PagingCollectionView<InformationEditModel> PagingCollection
    {
        get
        {
            if (_pagingCollection is null)
            {
                Refresh();
            }

            return _pagingCollection;
        }
        private set => Set(ref _pagingCollection, value);
    }

    public string ItemFilter
    {
        get => _search.Filter;
        set
        {
            string itemFilter = _search.Filter;
            Set(ref itemFilter, value);
            _search.Filter = itemFilter;
            Refresh();
        }
    }

    public string ItemSort
    {
        get => _itemSort;
        set => Set(ref _itemSort, value);
    }
    public bool? Active
    {
        get => _search.Active.HasValue ? (_search.Active.Value == ComicsStore.Data.Common.Active.active) : null;
        set
        {
            bool? active = _search.Active.HasValue ? (_search.Active.Value == ComicsStore.Data.Common.Active.active) : null;
            Set(ref active, value);
            _search.Active = active.HasValue ? (active.Value ? ComicsStore.Data.Common.Active.active : ComicsStore.Data.Common.Active.deleted) : null;
            Refresh();
        }
    }

    public IdSearch Search 
    { 
        get => _search;
        set
        {
            Set(ref _search, value);
            Refresh();
        }
    }
}
