using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BooksRepository : ComicsStoreMainRepository<Book>, IComicsStoreRepository<Book, BasicSearchModel>
    {
        public BooksRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Book> AddAsync(Book book)
        {
            return AddItemAsync(_context.Books, book);
        }

        public Task DeleteAsync(Book book)
        {
            return RemoveItemAsync(_context.Books, book);
        }

        public Task<List<Book>> GetAsync(BasicSearchModel model)
        {
            var books = _context.Books
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return books;
        }

        public Task<Book> GetAsync(int bookId)
        {
            return _context.Books.FindAsync(bookId);
        }

        public Task<Book> UpdateAsync(Book book)
        {
            return UpdateItemAsync(_context.Books, book);
        }
    }
}