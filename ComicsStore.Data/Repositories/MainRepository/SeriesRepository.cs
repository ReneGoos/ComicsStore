﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.Data.Repositories.MainRepository
{
    public class SeriesRepository : ComicsStoreMainRepository<Series, SeriesSearch>, IComicsStoreMainRepository<Series, SeriesSearch>
    {
        private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository;

        public SeriesRepository(ComicsStoreDbContext context,
                               IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository)
            : base(context)
        {
            _bookSeriesRepository = bookSeriesRepository;
        }

        public override Task<Series> AddAsync(Series value)
        {
            return AddItemAsync(_context.Series, value);
        }

        public override Task DeleteAsync(Series value)
        {
            return RemoveItemAsync(_context.Series, value);
        }

        public override Task<List<Series>> GetAsync()
        {
            var series = _context.Series
                .ToListAsync();

            return series;
        }

        public override Task<List<Series>> GetAsync(SeriesSearch model)
        {
            var series = _context.Series
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower()))
                .Where(s => !model.CodeId.HasValue || s.CodeId == model.CodeId)
                .ToListAsync();

            return series;
        }

        public override Task<Series> GetAsync(int seriesId, bool extended)
        {
            if (extended)
            {
                return _context.Series
                    .Include(s => s.BookSeries)
                    .ThenInclude(sb => sb.Book)
                    .Include(s => s.Code)
                    .SingleOrDefaultAsync(s => s.Id == seriesId);
            }

            return _context.Series
                .Include(s => s.BookSeries)
                .SingleOrDefaultAsync(s => s.Id == seriesId);
        }

        public override Task<Series> UpdateAsync(Series value)
        {
            return UpdateItemAsync(_context.Series, value, UpdateLinkedItems);
        }

        private bool UpdateLinkedItems(Series seriesCurrent, Series seriesNew)
        {
            _bookSeriesRepository.UpdateLinkedItems(seriesCurrent, seriesNew);

            return true;
        }

        public override Task<Series> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Series, id, data);
        }
    }
}