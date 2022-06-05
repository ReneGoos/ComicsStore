using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Core;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearch, BookEditModel>
    {
        public BookViewModel(IBooksService booksService,
            IMapper mapper) : base(booksService, mapper)
        {
        }

        public void AddBookPublisher(ObservableChangedCollection<BookPublisherEditModel> bookPublishers, int? publisherId)
        {
            Item.AddBookPublisher(bookPublishers, publisherId);
        }

        public void AddBookSeries(ObservableChangedCollection<BookSeriesEditModel> bookSeries, int? seriesId)
        {
            Item.AddBookSeries(bookSeries, seriesId);
        }

        public void AddStoryBook(ObservableChangedCollection<StoryBookEditModel> storyBooks, int? storyId)
        {
            Item.AddStoryBook(storyBooks, storyId);
        }

        public ObservableChangedCollection<BookPublisherEditModel> GetBookPublishers()
        {
            return Item.GetBookPublishers();
        }

        public ObservableChangedCollection<BookSeriesEditModel> GetBookSeries()
        {
            return Item.GetBookSeries();
        }

        public ObservableChangedCollection<StoryBookEditModel> GetStoryBooks()
        {
            return Item.GetStoryBooks();
        }
    }
}