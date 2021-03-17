using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BookSeriesRepository : ComicsStoreCrossRepository<BookSeries>,  IComicsStoreCrossRepository<BookSeries>
    {
        public BookSeriesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<BookSeries> AddAsync(BookSeries bookSeries)
        {
            return AddItemAsync(_context.BookSeries, bookSeries);
        }

        public Task<List<BookSeries>> AddAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(BookSeries bookSeries)
        {
            return RemoveItemAsync(_context.BookSeries, bookSeries);
        }

        public Task DeleteAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BookSeries>> GetAsync(int? id, int? crossId)
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

        public Task<BookSeries> UpdateAsync(BookSeries bookSeries)
        {
            return UpdateItemAsync(_context.BookSeries, bookSeries, bookSeries.BookId, bookSeries.SeriesId);
        }

        public Task<List<BookSeries>> UpdateAsync(IEnumerable<BookSeries> value)
        {
            throw new System.NotImplementedException();
        }
    }
}