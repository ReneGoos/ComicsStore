using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class BooksService : IBooksService
    {
        private readonly IComicsStoreRepository<Book, BasicSearchModel> _booksRepository;
        private readonly IComicsStoreCrossRepository<BookPublisher> _bookPublishersRepository;
        private readonly IComicsStoreCrossRepository<BookSeries> _bookSeriesRepository;

        public BooksService(IComicsStoreRepository<Book, BasicSearchModel> booksRepository,
            IComicsStoreCrossRepository<BookPublisher> bookPublishersRepository,
            IComicsStoreCrossRepository<BookSeries> bookSeriesRepository)
        {
            _booksRepository = booksRepository;
            _bookPublishersRepository = bookPublishersRepository;
            _bookSeriesRepository = bookSeriesRepository;
        }

        public async Task<BookOutputModel> AddAsync(BookInputModel bookInput)
        {
            var book = Mapper.Map<Book>(bookInput);

            var bookResult = await _booksRepository.AddAsync(book);

            return Mapper.Map<BookOutputModel>(bookResult);
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

        public async Task<List<BookOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var books =  await _booksRepository.GetAsync(searchModel);

            return Mapper.Map<List<BookOutputModel>>(books);
        }

        public async Task<BookOutputModel> GetAsync(int id)
        {
            var book = await _booksRepository.GetAsync(id);

            return Mapper.Map<BookOutputModel>(book);
        }

        public async Task<BookOutputModel> UpdateAsync(int id, BookInputModel bookInput)
        {
            var book = Mapper.Map<Book>(bookInput);
            book.Id = id;

            book = await _booksRepository.UpdateAsync(book);

            return Mapper.Map<BookOutputModel>(book);
        }

        public async Task<BookOutputModel> PatchAsync(int id, BookInputPatchModel bookInput)
        {
            var modifiedData = JsonHelper.ModifiedData<BookInputPatchModel, Book>(bookInput);

            var book = await _booksRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<BookOutputModel>(book);
        }

        public async Task<List<BookPublisherOutputModel>> GetPublishersAsync(int bookId)
        {
            var bookPublishers = await _bookPublishersRepository.GetAsync(bookId, null);

            try
            {
                return Mapper.Map<List<BookPublisherOutputModel>>(bookPublishers);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
