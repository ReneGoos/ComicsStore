using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

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

        public Task DeleteAsync(BookSeries bookSeries)
        {
            return RemoveItemAsync(_context.BookSeries, bookSeries);
        }
        
        public Task<BookSeries> UpdateAsync(BookSeries bookSeries)
        {
            return UpdateItemAsync(_context.BookSeries, bookSeries, bookSeries.BookId, bookSeries.SeriesId);
        }
    }
}