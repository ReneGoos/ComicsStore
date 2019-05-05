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
                          join story in _context.Stories on storySeries.StoryId equals story.Id
                          join storyCharacter in _context.StoryCharacters on story.Id equals storyCharacter.StoryId
                          join character in _context.Characters on storyCharacter.CharacterId equals character.Id
                          join storyArtist in _context.StoryArtists on story.Id equals storyArtist.StoryId
                          join artist in _context.Artists on storyArtist.ArtistId equals artist.Id
                          join publisher in _context.Publishers on storySeries.PublisherId equals publisher.Id
                          where !model.Active.HasValue || storySeries.Deleted == model.Active.Value
                          orderby story.Name,
                          story.StoryNumber,
                          story.StoryType,
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
                              Title = story.Name,
                              StoryNumber = story.StoryNumber,
                              ExtraInfo = story.ExtraInfo,
                              StoryType = story.StoryType,
                              BookType = storySeries.BookType,
                              Character = character.Name,
                              Artist = artist.Name,
                              ArtistType = storyArtist.ArtistType,
                              Issue = storySeries.Issue,
                              IssueTitle = storySeries.IssueTitle,
                              Language = storySeries.Language,
                              Series = storySeries.SeriesName,
                              Publisher = publisher.Name,
                              Year = storySeries.Year,
                              PurchaseDate = storySeries.PurchaseDate,
                              Notes = story.ExtraInfo,
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
