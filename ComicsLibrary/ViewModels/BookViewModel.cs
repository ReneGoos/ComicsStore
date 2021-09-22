using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearch, BookEditModel>
    {
        public BookViewModel(IBooksService booksService,
            IMapper mapper) : base(booksService, mapper)
        {
        }

        public void AddBookPublisher(int? publisherId)
        {
            Item.AddBookPublisher(publisherId);
        }

        public void AddBookSeries(int? seriesId)
        {
            Item.AddBookSeries(seriesId);
        }

        public void AddStoryBook(int? storyId)
        {
            Item.AddStoryBook(storyId);
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