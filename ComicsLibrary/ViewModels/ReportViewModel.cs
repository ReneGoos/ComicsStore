using AutoMapper;
using ComicsLibrary.Core;
using ComicsLibrary.EditModels;
using ComicsLibrary.Helpers;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Reports;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace ComicsLibrary.ViewModels
{
    public class ReportViewModel : BasicEditModel
    {
        private readonly IExportBooksService _exportBooksService;
        private readonly IMapper _mapper;
        private PagingCollectionView<ReportEditModel> _pagingCollection;

        private bool? _active = true;
        private string _itemFilter;
        private string _itemSort;

        public ICommand StoreReportWindowCommand { get; protected set; }

        public ReportViewModel(IExportBooksService exportBooksService,
                                IMapper mapper) : base()
        {
            _exportBooksService = exportBooksService;
            _mapper = mapper;

            StoreReportWindowCommand = new RelayCommand(new Action(StoreReportWindow));
        }

        private async void StoreReportWindow()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                var report = await _exportBooksService.GetExportAsync(new StorySeriesSearch
                {
                    Filter = _itemFilter,
                    Active = _active.HasValue ? (_active.Value ? ComicsStore.Data.Common.Active.active : ComicsStore.Data.Common.Active.deleted) : null
                });

                await File.WriteAllTextAsync(saveFileDialog.FileName, report);
            }
        }

        private async void Refresh()
        {
            var list = _mapper.Map<List<ReportEditModel>>(await _exportBooksService.GetAsync(new StorySeriesSearch
            {
                Filter = _itemFilter,
                Active = _active.HasValue ? (_active.Value ? ComicsStore.Data.Common.Active.active : ComicsStore.Data.Common.Active.deleted) : null
            })); ; ;
            PagingCollection = new PagingCollectionView<ReportEditModel>(list, 50);
        }

        public PagingCollectionView<ReportEditModel> PagingCollection
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
            get => _itemFilter;
            set
            {
                Set(ref _itemFilter, value);
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
            get => _active;
            set
            {
                Set(ref _active, value);
                Refresh();
            }
        }
    }
}
