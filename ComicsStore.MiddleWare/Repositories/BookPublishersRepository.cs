using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BookPublishersRepository : ComicsStoreCrossRepository<BookPublisher, IBookPublisher>,  IComicsStoreCrossRepository<BookPublisher, IBookPublisher>
    {
        public BookPublishersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<BookPublisher> AddAsync(BookPublisher bookPublisher)
        {
            return AddItemAsync(_context.BookPublishers, bookPublisher);
        }

        public override Task<List<BookPublisher>> AddAsync(IEnumerable<BookPublisher> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(BookPublisher bookPublisher)
        {
            return RemoveItemAsync(_context.BookPublishers, bookPublisher);
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

        public override Task<BookPublisher> UpdateAsync(BookPublisher bookPublisher)
        {
            return UpdateItemAsync(_context.BookPublishers, bookPublisher, bookPublisher.BookId, bookPublisher.PublisherId);
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
                foreach (var existingChild in itemCurrent.BookPublisher)
                {
                    if (!itemNew.BookPublisher.Any(c => c.PublisherId == existingChild.PublisherId && c.BookId == existingChild.BookId))
                    {
                        _context.BookPublishers.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.BookPublisher)
                {
                    var existingChild = itemCurrent.BookPublisher
                        .Where(c => c.PublisherId == childModel.PublisherId
                             && c.BookId == childModel.BookId
                             && c.BookId != default
                             && c.PublisherId != default)
                        .SingleOrDefault();

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