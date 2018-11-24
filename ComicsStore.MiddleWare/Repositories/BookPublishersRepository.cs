using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

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

        public Task DeleteAsync(BookPublisher bookPublisher)
        {
            return RemoveItemAsync(_context.BookPublishers, bookPublisher);
        }
        
        public Task<BookPublisher> UpdateAsync(BookPublisher bookPublisher)
        {
            return UpdateItemAsync(_context.BookPublishers, bookPublisher, bookPublisher.BookId, bookPublisher.PublisherId);
        }
    }
}