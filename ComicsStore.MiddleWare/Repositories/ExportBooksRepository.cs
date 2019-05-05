using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class ExportBooksRepository : IExportBooksRepository
    {
        protected readonly ComicsStoreDbContext _context;

        public ExportBooksRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportBook>> GetAsync(StorySeriesSearchModel model)
        {
            var exports = _context.ExportBooks
                .Where(s => !model.Active.HasValue || s.Deleted == model.Active.Value)
                .OrderBy(e => e.Title)
                .ThenBy(e => e.StoryNumber)
                .ThenBy(e => e.StoryType)
                .ThenBy(e => e.Issue)
                .ThenBy(e => e.IssueTitle)
                .ThenBy(e => e.StoryId)
                .ThenBy(e => e.BookId)
                .ThenBy(e => e.SeriesId)
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }

        public Task<List<ExportStory>> GetStoryAsync(StorySeriesSearchModel model)
        {
            var exports = _context.ExportStory
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }
    }
}
