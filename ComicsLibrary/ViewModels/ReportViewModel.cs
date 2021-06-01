using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsLibrary.Helpers;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;

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

        public ReportViewModel(IExportBooksService exportBooksService,
                                IMapper mapper) : base()
        {
            _exportBooksService = exportBooksService;
            _mapper = mapper;
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
            private set
            {
                Set(ref _pagingCollection, value);
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
                Refresh();
            }
        }

        public string ItemSort
        {
            get
            {
                return _itemSort;
            }
            set
            {
                Set(ref _itemSort, value);
            }
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
