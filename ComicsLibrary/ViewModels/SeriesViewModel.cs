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

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicTableViewModel<ISeriesService, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch, SeriesEditModel>
    {
        public ICommand DeleteBookFromListCommand { get; protected set; }

        public SeriesViewModel(ISeriesService seriesService,
            INavigationService navigationService,
            IMapper mapper) : base(seriesService, navigationService, mapper)
        {
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
        }

        public void AddSeriesBook(int? bookId, int? oldBookId)
        {
            Item.AddBookSeries(bookId, oldBookId);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.AddBookSeries(null, bookId);
        }

        public void AddSeriesCode(int? codeId, int? oldCodeId)
        {
            Item.AddSeriesCode(codeId);
        }
    }
}