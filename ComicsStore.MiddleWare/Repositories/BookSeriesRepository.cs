using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BookSeriesRepository : ComicsStoreCrossRepository<BookSeries, IBookSeries>,  IComicsStoreCrossRepository<BookSeries, IBookSeries>
    {
        public BookSeriesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<BookSeries> AddAsync(BookSeries bookSeries)
        {
            return AddItemAsync(_context.BookSeries, bookSeries);
        }

        public override Task<List<BookSeries>> AddAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(BookSeries bookSeries)
        {
            return RemoveItemAsync(_context.BookSeries, bookSeries);
        }

        public override Task DeleteAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<BookSeries>> GetAsync()
        {
            return _context.BookSeries
                .ToListAsync();
        }

        public override Task<List<BookSeries>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
            {
                return null;
            }

            return _context.BookSeries
                .Include(sa => sa.Series)
                .ThenInclude(s => s.Code)
                .Include(sa => sa.Book)
                .Where(s => id != null ? s.BookId == id : s.SeriesId == crossId)
                .ToListAsync();
        }

        public override Task<BookSeries> UpdateAsync(BookSeries bookSeries)
        {
            return UpdateItemAsync(_context.BookSeries, bookSeries, bookSeries.BookId, bookSeries.SeriesId);
        }

        public override Task<List<BookSeries>> UpdateAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLinkedItems(IBookSeries itemCurrent, IBookSeries itemNew)
        {
            if (itemNew.BookSeries is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.BookSeries)
                {
                    if (!itemNew.BookSeries.Any(c => c.SeriesId == existingChild.SeriesId && c.BookId == existingChild.BookId))
                    {
                        itemCurrent.BookSeries.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.BookSeries)
                {
                    var existingChild = itemCurrent.BookSeries
                        .Where(c => c.SeriesId == childModel.SeriesId && c.BookId == childModel.BookId && c.BookId != default && c.SeriesId != default)
                        .SingleOrDefault();

                    if (existingChild != null)
                    {
                        if ((existingChild.Issue ?? "") != (childModel.Issue ?? "")
                            || existingChild.SeriesOrder.HasValue != childModel.SeriesOrder.HasValue
                            || existingChild.SeriesOrder != childModel.SeriesOrder)
                        {
                            // Remove child
                            itemCurrent.BookSeries.Remove(existingChild);

                            // Insert child
                            var newChild = new BookSeries
                            {
                                CreationDate = existingChild.CreationDate,
                                SeriesId = childModel.SeriesId,
                                BookId = childModel.BookId,
                                Issue = childModel.Issue,
                                SeriesOrder = childModel.SeriesOrder
                            };
                            itemCurrent.BookSeries.Add(newChild);
                        }
                    }
                    else if (childModel.SeriesId > 0)
                    {
                        // Insert child
                        var newChild = new BookSeries
                        {
                            SeriesId = childModel.SeriesId,
                            BookId = childModel.BookId,
                            Issue = childModel.Issue,
                            SeriesOrder = childModel.SeriesOrder
                        };
                        itemCurrent.BookSeries.Add(newChild);
                    }
                }
            }
        }
    }
}