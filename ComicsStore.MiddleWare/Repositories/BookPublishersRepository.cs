using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BookPublishersRepository : ComicsStoreCrossRepository<BookPublisher>,  IComicsStoreCrossRepository<BookPublisher>
    {
        public BookPublishersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<BookPublisher> AddAsync(BookPublisher bookPublisher)
        {
            return AddItemAsync(_context.BookPublishers, bookPublisher);
        }

        public Task<List<BookPublisher>> AddAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(BookPublisher bookPublisher)
        {
            return RemoveItemAsync(_context.BookPublishers, bookPublisher);
        }

        public Task DeleteAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<BookPublisher> UpdateAsync(BookPublisher bookPublisher)
        {
            return UpdateItemAsync(_context.BookPublishers, bookPublisher, bookPublisher.BookId, bookPublisher.PublisherId);
        }

        public Task<List<BookPublisher>> UpdateAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BookPublisher>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
            {
                return null;
            }

            return _context.BookPublishers
                .Include(sa => sa.Publisher)
                .Include(sa => sa.Book)
                .Where(s => id != null ? s.BookId == id : s.PublisherId == crossId)
                .ToListAsync();
        }
    }
}