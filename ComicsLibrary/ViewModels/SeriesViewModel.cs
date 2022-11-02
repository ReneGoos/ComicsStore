using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.MiddleWare.Services;

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicTableViewModel<ISeriesService, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch, SeriesEditModel>
    {
        private readonly IBooksService _booksService;
        private readonly ICodesService _codesService;

        public ICommand DeleteBookFromListCommand { get; protected set; }

        public SeriesViewModel(ISeriesService seriesService,
            IBooksService booksService,
            ICodesService codesService,
            INavigationService navigationService,
            IMapper mapper) : base(seriesService, navigationService, mapper)
        {
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
            _booksService = booksService;
            _codesService = codesService;
        }

        public async void HandleBook(int? bookId, int? oldBookId)
        {
            var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
            Item.HandleBook(oldBookId, book);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.HandleBook(bookId, null);
        }

        public void HandleCode(int? codeId, int? oldCodeId)
        {
            Item.HandleCode(codeId);
        }
    }
}