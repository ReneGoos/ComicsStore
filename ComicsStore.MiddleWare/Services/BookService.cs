using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services
{
    public class BooksService : ComicsStoreService<Book, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearch>, IBooksService
    {
        private readonly IComicsStoreCrossRepository<BookPublisher, IBookPublisher> _bookPublishersRepository;
        private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository;
        private readonly IComicsStoreCrossRepository<StoryBook, IStoryBook> _storyBooksRepository;

        public BooksService(IComicsStoreMainRepository<Book, BasicSearch> booksRepository,
            IComicsStoreCrossRepository<BookPublisher, IBookPublisher> bookPublishersRepository,
            IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository,
            IComicsStoreCrossRepository<StoryBook, IStoryBook> storyBooksRepository,
            IMapper mapper) : base(booksRepository, mapper)
        {
            _bookPublishersRepository = bookPublishersRepository;
            _bookSeriesRepository = bookSeriesRepository;
            _storyBooksRepository = storyBooksRepository;
        }

        public async Task<ICollection<BookPublisherOutputModel>> GetPublishersAsync(int bookId)
        {
            var bookPublishers = await _bookPublishersRepository.GetAsync(bookId, null);

            try
            {
                return Mapper.Map<ICollection<BookPublisherOutputModel>>(bookPublishers);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<BookSeriesOutputModel>> GetSeriesAsync(int bookId)
        {
            var bookSeries = await _bookSeriesRepository.GetAsync(bookId, null);

            try
            {
                return Mapper.Map<ICollection<BookSeriesOutputModel>>(bookSeries);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<BookStoryOutputModel>> GetStoriesAsync(int bookId)
        {
            var bookStories = await _storyBooksRepository.GetAsync(null, bookId);

            try
            {
                return Mapper.Map<ICollection<BookStoryOutputModel>>(bookStories);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
