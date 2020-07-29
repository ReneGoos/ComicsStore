using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class SeriesRepository : ComicsStoreMainRepository<Series>, IComicsStoreRepository<Series, SeriesSearchModel>
    {
        public SeriesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Series> AddAsync(Series series)
        {
            return AddItemAsync(_context.Series, series);
        }

        public Task DeleteAsync(Series series)
        {
            return RemoveItemAsync(_context.Series, series);
        }

        public Task<List<Series>> GetAsync(SeriesSearchModel model)
        {
            var series = _context.Series
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower()))
                .Where(s => !model.CodeId.HasValue || s.CodeId == model.CodeId)
                .ToListAsync();

            return series;
        }

        public Task<Series> GetAsync(int seriesId)
        {
            return _context.Series
                .Include(s => s.BookSeries)
                .SingleOrDefaultAsync(s => s.Id == seriesId);
        }

        public Task<Series> UpdateAsync(Series series)
        {
            return UpdateItemAsync(_context.Series, series);
        }

        public Task<Series> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Series, id, data);
        }
    }
}