using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class BooksService : IBooksService
    {
        private readonly IComicsStoreRepository<Book, BasicSearchModel> _booksRepository;
        private readonly IComicsStoreCrossRepository<BookPublisher> _bookPublishersRepository;
        private readonly IComicsStoreCrossRepository<BookSeries> _bookSeriesRepository;
        private readonly IComicsStoreCrossRepository<StoryBook> _storyBooksRepository;
        private readonly IMapper _mapper;

        public BooksService(IComicsStoreRepository<Book, BasicSearchModel> booksRepository,
            IComicsStoreCrossRepository<BookPublisher> bookPublishersRepository,
            IComicsStoreCrossRepository<BookSeries> bookSeriesRepository,
            IComicsStoreCrossRepository<StoryBook> storyBooksRepository,
            IMapper mapper)
        {
            _booksRepository = booksRepository;
            _bookPublishersRepository = bookPublishersRepository;
            _bookSeriesRepository = bookSeriesRepository;
            _storyBooksRepository = storyBooksRepository;
            _mapper = mapper;
        }

        public async Task<BookOutputModel> AddAsync(BookInputModel bookInput)
        {
            var book = _mapper.Map<Book>(bookInput);

            var bookResult = await _booksRepository.AddAsync(book);

            return _mapper.Map<BookOutputModel>(bookResult);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _booksRepository.GetAsync(id);

            if (book == null)
            {
                return;
            }

            await _booksRepository.DeleteAsync(book);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _booksRepository.GetAsync(id) != null;
        }

        public async Task<ICollection<BookOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var books =  await _booksRepository.GetAsync(searchModel);

            return _mapper.Map<ICollection<BookOutputModel>>(books);
        }

        public async Task<BookOutputModel> GetAsync(int id)
        {
            var book = await _booksRepository.GetAsync(id);

            return _mapper.Map<BookOutputModel>(book);
        }

        public async Task<BookOutputModel> UpdateAsync(int id, BookInputModel bookInput)
        {
            var book = _mapper.Map<Book>(bookInput);
            book.Id = id;

            book = await _booksRepository.UpdateAsync(book);

            return _mapper.Map<BookOutputModel>(book);
        }

        public async Task<BookOutputModel> PatchAsync(int id, BookInputPatchModel bookInput)
        {
            var modifiedData = JsonHelper.ModifiedData<BookInputPatchModel, Book>(bookInput, _mapper);

            var book = await _booksRepository.PatchAsync(id, modifiedData);

            return _mapper.Map<BookOutputModel>(book);
        }

        public async Task<ICollection<BookPublisherOutputModel>> GetPublishersAsync(int bookId)
        {
            var bookPublishers = await _bookPublishersRepository.GetAsync(bookId, null);

            try
            {
                return _mapper.Map<ICollection<BookPublisherOutputModel>>(bookPublishers);
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
                return _mapper.Map<ICollection<BookSeriesOutputModel>>(bookSeries);
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
                return _mapper.Map<ICollection<BookStoryOutputModel>>(bookStories);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
