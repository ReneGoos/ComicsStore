using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class BooksService : IComicsStoreService<BookInputModel, BookOutputModel, BasicSearchModel>
    {
        private readonly IComicsStoreRepository<Book, BasicSearchModel> _booksRepository;

        public BooksService(IComicsStoreRepository<Book, BasicSearchModel> booksRepository)
        {
            _booksRepository = booksRepository;
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

        public async Task<IEnumerable<BookOutputModel>> GetAsync(BasicSearchModel searchModel)
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
    }
}
