using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class StorySeriesRepository : IExportBooksRepository
    {
        protected readonly ComicsStoreDbContext _context;

        public StorySeriesRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportBook>> GetAsync(StorySeriesSearchModel model)
        {
            var exports = from storySeries in _context.StorySeries
                          where !model.Active.HasValue || storySeries.Deleted == model.Active.Value
                          orderby storySeries.StoryName,
                          storySeries.StoryNumber,
                          storySeries.StoryType,
                          storySeries.Issue,
                          storySeries.IssueTitle,
                          storySeries.StoryId,
                          storySeries.BookId,
                          storySeries.SeriesId
                          select new ExportBook
                          {
                              StoryId = storySeries.StoryId,
                              BookId = storySeries.BookId,
                              SeriesId = storySeries.SeriesId,
                              /*
                               CodeId = storySeries.CodeId,
                              MinSeriesOrder = storySeries.MinSeriesOrder,
                              MaxSeriesOrder = storySeries.MaxSeriesOrder,
                              */
                              Title = storySeries.StoryName,
                              StoryNumber = storySeries.StoryNumber,
                              ExtraInfo = storySeries.ExtraInfo,
                              StoryType = storySeries.StoryType,
                              BookType = storySeries.BookType,
                              Character = storySeries.CharacterName,
                              Artist = storySeries.ArtistName,
                              ArtistType = storySeries.ArtistType,
                              Issue = storySeries.Issue,
                              IssueTitle = storySeries.IssueTitle,
                              Language = storySeries.Language,
                              Series = storySeries.SeriesName,
                              Publisher = storySeries.PublisherName,
                              Year = storySeries.Year,
                              PurchaseDate = storySeries.PurchaseDate,
                              Notes = storySeries.ExtraInfo,
                              Deleted = storySeries.Deleted
                          };

            return exports
                .ToListAsync();
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
