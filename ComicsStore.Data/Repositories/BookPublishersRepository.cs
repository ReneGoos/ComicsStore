using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class BookPublishersRepository : ComicsStoreCrossRepository<BookPublisher, IBookPublisher>, IComicsStoreCrossRepository<BookPublisher, IBookPublisher>
    {
        public BookPublishersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<BookPublisher> AddAsync(BookPublisher value)
        {
            return AddItemAsync(_context.BookPublishers, value);
        }

        public override Task<List<BookPublisher>> AddAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(BookPublisher value)
        {
            return RemoveItemAsync(_context.BookPublishers, value);
        }

        public override Task DeleteAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<BookPublisher>> GetAsync()
        {
            return _context.BookPublishers
                .ToListAsync();
        }

        public override Task<List<BookPublisher>> GetAsync(int? id, int? crossId)
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

        public override Task<BookPublisher> UpdateAsync(BookPublisher value)
        {
            return UpdateItemAsync(_context.BookPublishers, value, value.BookId, value.PublisherId);
        }

        public override Task<List<BookPublisher>> UpdateAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLinkedItems(IBookPublisher itemCurrent, IBookPublisher itemNew)
        {
            if (itemNew.BookPublisher is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.BookPublisher.ToList())
                {
                    if (!itemNew.BookPublisher.Any(c => c.PublisherId == existingChild.PublisherId && c.BookId == existingChild.BookId))
                    {
                        _ = _context.BookPublishers.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.BookPublisher.ToList())
                {
                    var existingChild = itemCurrent.BookPublisher
                        .SingleOrDefault(c => c.PublisherId == childModel.PublisherId
                             && c.BookId == childModel.BookId
                             && c.BookId != default
                             && c.PublisherId != default);

                    if (existingChild is null && childModel.PublisherId > 0 && childModel.BookId > 0)
                    {
                        // Insert child
                        var newChild = new BookPublisher
                        {
                            PublisherId = childModel.PublisherId,
                            BookId = childModel.BookId
                        };
                        itemCurrent.BookPublisher.Add(newChild);
                    }
                }
            }
        }
    }
}