using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Repositories
{
    public class ExportBooksRepository : IExportBooksRepository
    {
        protected readonly ComicsStoreDbContext _context;

        public ExportBooksRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportBook>> GetAsync(ExportBooksSearchModel model)
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
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }

        public Task<List<ExportStory>> GetStoryAsync(ExportBooksSearchModel model)
        {
            var exports = _context.ExportStory
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }
    }
}
