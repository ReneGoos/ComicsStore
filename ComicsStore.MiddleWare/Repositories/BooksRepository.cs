using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;

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
                .Include(b => b.StoryBook)
                .Include(b => b.BookSeries)
                .Include(b => b.BookPublisher)
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return books;
        }

        public Task<Book> GetAsync(int bookId)
        {
            return _context.Books
                .Include(b => b.StoryBook)
                .ThenInclude(sb => sb.Story)
                .Include(b => b.BookSeries)
                .ThenInclude(bs => bs.Series)
                .ThenInclude(s => s.Code)
                .Include(b => b.BookPublisher)
                .ThenInclude(bp => bp.Publisher)
                .SingleOrDefaultAsync(b => b.Id == bookId);
        }

        public Task<Book> UpdateAsync(Book book)
        {
            return UpdateItemAsync(_context.Books, book);
        }

        public Task<Book> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Books, id, data);
        }
    }
}