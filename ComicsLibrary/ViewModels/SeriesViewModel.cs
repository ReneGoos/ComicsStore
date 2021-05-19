using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicTableViewModel<ISeriesService, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearchModel, SeriesEditModel>
    {
        public SeriesViewModel(ISeriesService seriesService,
            IMapper mapper) : base(seriesService, mapper)
        {
        }

        public void AddSeriesCode(int? codeId)
        {
            Item.AddSeriesCode(codeId);
        }

        public void AddBookSeries(List<BookSeriesEditModel> bookSeries, int? bookId)
        {
            Item.AddBookSeries(bookSeries, bookId);
        }

        public List<BookSeriesEditModel> GetBookSeries()
        {
            return Item.GetBookSeries();
        }
    }
}