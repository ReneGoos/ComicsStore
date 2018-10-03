using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class SeriesRepository : ComicsStoreRepository, IComicsStoreRepository<Series, BasicSearchModel>
    {
        public SeriesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Series> AddAsync(Series series)
        {
            var seriesEntity = await _context.Series.AddAsync(series);

            await SaveChangesAsync();

            return seriesEntity.Entity;
        }

        public async Task DeleteAsync(Series series)
        {
            _context.Series.Remove(series);

            await SaveChangesAsync();
        }

        public Task<List<Series>> GetAsync(BasicSearchModel model)
        {
            var series = _context.Series
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return series;
        }

        public Task<Series> GetAsync(int seriesId)
        {
            return _context.Series.SingleOrDefaultAsync(s => s.Id == seriesId);
        }

        public async Task<Series> UpdateAsync(Series series)
        {
            var seriesEntity = _context.Series.Update(series);

            await SaveChangesAsync();

            return seriesEntity.Entity;
        }
    }
}