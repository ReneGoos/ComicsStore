using ComicsStore.Data.Model.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Output;

namespace ComicsStore.Data.Repositories.ViewRepository
{
    public class ExportBooksViewRepository : IViewRepository<ExportBook, ViewSearch>
    {
        protected readonly ComicsStoreDbContext _context;

        public ExportBooksViewRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportBook>> GetAsync(ViewSearch model)
        {
            var exports = _context.ExportBooks
                .Where(s => (!model.Active.HasValue || s.Deleted == model.Active.Value)
                            && (model.Filter == null || model.Filter.Length == 0 || s.Title.ToLower().Contains(model.Filter.ToLower())))
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

        /*
        public Task<List<ExportStory>> GetStoryAsync(ViewSearch model)
        {
            var exports = _context.ExportStory
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }
        */
    }
}
