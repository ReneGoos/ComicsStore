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
    public class ExportMementoRepository : IExportMementoRepository
    {
        protected readonly ComicsStoreDbContext _context;

        public ExportMementoRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportMemento>> GetAsync(ExportMementoSearchModel model)
        {
            var exports = _context.ExportMemento
                .Where(s => !model.Active.HasValue || s.Deleted == model.Active.Value)
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }

        public Task<List<ExportStory>> GetStoryAsync(ExportMementoSearchModel model)
        {
            var exports = _context.ExportStory
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }
    }
}
