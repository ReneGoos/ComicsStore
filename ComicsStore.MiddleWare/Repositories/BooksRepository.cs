using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BooksRepository : ComicsStoreRepository, IComicsStoreRepository<Book, BasicSearchModel>
    {
        public BooksRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Book> AddAsync(Book book)
        {
            var bookEntity = await _context.Books.AddAsync(book);

            await SaveChangesAsync();

            return bookEntity.Entity;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);

            await SaveChangesAsync();
        }

        public Task<List<Book>> GetAsync(BasicSearchModel model)
        {
            var books = _context.Books
                .Where(s => model.Name == null || s.BookName.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return books;
        }

        public Task<Book> GetAsync(int bookId)
        {
            return _context.Books.SingleOrDefaultAsync(s => s.Id == bookId);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var bookEntity = _context.Books.Update(book);

            await SaveChangesAsync();

            return bookEntity.Entity;
        }
    }
}