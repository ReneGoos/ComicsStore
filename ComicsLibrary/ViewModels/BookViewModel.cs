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
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearchModel, BookEditModel>
    {
        public BookViewModel(IBooksService booksService,
            IMapper mapper) : base(booksService, mapper)
        {
        }

        public void AddBookSeries(ICollection<BookSeriesEditModel> bookSeries, int? seriesId)
        {
            Item.AddBookSeries(bookSeries, seriesId);
        }

        public void AddBookPublisher(ICollection<BookPublisherEditModel> bookPublishers, int? publisherId)
        {
            Item.AddBookPublisher(bookPublishers, publisherId);
        }
    }
}