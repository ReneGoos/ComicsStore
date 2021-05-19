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

        public void AddBookPublisher(List<BookPublisherEditModel> bookPublishers, int? publisherId)
        {
            Item.AddBookPublisher(bookPublishers, publisherId);
        }

        public void AddBookSeries(List<BookSeriesEditModel> bookSeries, int? seriesId)
        {
            Item.AddBookSeries(bookSeries, seriesId);
        }

        public void AddStoryBook(List<StoryBookEditModel> storyBooks, int? storyId)
        {
            Item.AddStoryBook(storyBooks, storyId);
        }

        public List<BookPublisherEditModel> GetBookPublishers()
        {
            return Item.GetBookPublishers();
        }

        public List<BookSeriesEditModel> GetBookSeries()
        {
            return Item.GetBookSeries();
        }

        public List<StoryBookEditModel> GetStoryBooks()
        {
            return Item.GetStoryBooks();
        }
    }
}