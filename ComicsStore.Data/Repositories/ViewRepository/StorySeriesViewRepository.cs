using ComicsStore.Data.Model.Search;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Output;

namespace ComicsStore.Data.Repositories.CrossRepository
{
    public class StorySeriesViewRepository : IViewRepository<ExportBook, ViewSearch>
    {
        protected readonly ComicsStoreDbContext _context;

        public StorySeriesViewRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        public Task<List<ExportBook>> GetAsync(ViewSearch model)
        {
            var exports = from storySeries in _context.StorySeries
                          where (!model.Active.HasValue || storySeries.Deleted == model.Active.Value)
                          && (model.Filter == null ||
                                model.Filter.Length == 0 ||
                                storySeries.StoryName.ToLower().Contains(model.Filter.ToLower()) ||
                                storySeries.SeriesName.ToLower().Contains(model.Filter.ToLower()) ||
                                storySeries.CharacterName.ToLower().Contains(model.Filter.ToLower()) ||
                                storySeries.ArtistName.ToLower().Contains(model.Filter.ToLower())
                                )
                          orderby storySeries.StoryCode, 
                          storySeries.StoryType,
                          storySeries.StoryNumber,
                          storySeries.StoryName,
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
                              OriginalTitle = storySeries.OriginalStoryName,
                              StoryNumber = storySeries.StoryNumber,
                              ExtraInfo = storySeries.ExtraInfo,
                              StoryType = storySeries.StoryType,
                              BookType = storySeries.BookType,
                              Character = storySeries.CharacterName,
                              StoryCode = storySeries.StoryCode,
                              Artist = storySeries.ArtistName,
                              ArtistType = storySeries.ArtistType == 0 ? ArtistType.translator : storySeries.ArtistType,
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

        public Task<List<ExportStory>> GetStoryAsync(ViewSearch model)
        {
            var exports = _context.ExportStory
                .AsNoTracking()
                .ToListAsync();

            return exports;
        }
    }
}
