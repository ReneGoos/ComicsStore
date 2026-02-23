using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.Data.Repositories.ViewRepository;
public class InformationViewRepository(ComicsStoreDbContext context) : IViewRepository<ExportBook, IdSearch>
{
    protected readonly ComicsStoreDbContext _context = context;

    public Task<List<ExportBook>> GetAsync(IdSearch model)
    {
        /*
        var exports = _context.ExportBooks
            .Where(s => (!model.BookId.HasValue || s.BookId == model.BookId.Value))
            .OrderBy(e => e.Title)
            .ThenBy(e => e.StoryNumber)
            .ThenBy(e => e.StoryType)
            .ThenBy(e => e.Issue)
            .ThenBy(e => e.IssueTitle)
            .ThenBy(e => e.StoryId)
            .ThenBy(e => e.BookId)
            .ThenBy(e => e.SeriesId)
            .AsNoTracking();
        */
        var lowerFilter = model.Filter?.ToLower();

        var exports = from comicsInformation in _context.Information
                      where (!model.Active.HasValue || comicsInformation.Deleted == model.Active.Value) &&
                            (lowerFilter == null ||
                            lowerFilter.Length == 0 ||
                            comicsInformation.StoryName.ToLower().Contains(lowerFilter) ||
                            comicsInformation.OriginalStoryName.ToLower().Contains(lowerFilter) ||
                            comicsInformation.ExtraInfo.ToLower().Contains(lowerFilter) ||
                            comicsInformation.IssueTitle.ToLower().Contains(lowerFilter) ||
                            comicsInformation.SeriesName.ToLower().Contains(lowerFilter) ||
                            comicsInformation.CharacterName.ToLower().Contains(lowerFilter) ||
                            comicsInformation.PublisherName.ToLower().Contains(lowerFilter) ||
                            comicsInformation.ArtistName.ToLower().Contains(lowerFilter)) &&
                            (!model.ArtistId.HasValue || comicsInformation.ArtistId == model.ArtistId.Value) &&
                            (!model.BookId.HasValue || comicsInformation.BookId == model.BookId.Value) &&
                            (!model.CharacterId.HasValue || comicsInformation.CharacterId == model.CharacterId.Value) &&
                            (!model.CodeId.HasValue || comicsInformation.StoryCodeId == model.CodeId.Value || comicsInformation.SeriesCodeId == model.CodeId.Value) &&
                            (!model.PublisherId.HasValue || comicsInformation.PublisherId == model.PublisherId.Value) &&
                            (!model.SeriesId.HasValue || comicsInformation.SeriesId == model.SeriesId.Value) &&
                            (!model.StoryId.HasValue || comicsInformation.StoryId == model.StoryId.Value || comicsInformation.OriginStoryId == model.StoryId.Value) && 
                            ((!model.BookId.HasValue && !comicsInformation.PeriodicalGroups.Equals("U")) || (model.BookId.HasValue && !comicsInformation.PeriodicalGroups.Equals("G")))
                      orderby comicsInformation.StoryCode,
                      comicsInformation.StoryType,
                      comicsInformation.StoryNumber,
                      comicsInformation.StoryName,
                      comicsInformation.Issue,
                      comicsInformation.IssueTitle,
                      comicsInformation.StoryId,
                      comicsInformation.BookId,
                      comicsInformation.SeriesId
                      select new ExportBook
                      {
                          StoryId = comicsInformation.StoryId,
                          BookId = comicsInformation.BookId,
                          SeriesId = comicsInformation.SeriesId,
                          /*
                          CodeId = comicsInformation.CodeId,
                          MinSeriesOrder = comicsInformation.MinSeriesOrder,
                          MaxSeriesOrder = comicsInformation.MaxSeriesOrder,
                          */
                          Title = comicsInformation.StoryName,
                          OriginalTitle = comicsInformation.OriginalStoryName,
                          StoryNumber = comicsInformation.StoryNumber,
                          ExtraInfo = comicsInformation.ExtraInfo,
                          StoryType = comicsInformation.StoryType,
                          BookType = comicsInformation.BookType,
                          Character = comicsInformation.CharacterName,
                          StoryCode = comicsInformation.StoryCode,
                          Artist = comicsInformation.ArtistName,
                          ArtistType = comicsInformation.ArtistType == 0 ? ArtistType.translator : comicsInformation.ArtistType,
                          Issue = comicsInformation.Issue,
                          IssueTitle = comicsInformation.IssueTitle,
                          Language = comicsInformation.Language,
                          Series = comicsInformation.SeriesName,
                          Publisher = comicsInformation.PublisherName,
                          Year = comicsInformation.Year,
                          PurchaseDate = comicsInformation.PurchaseDate,
                          Notes = comicsInformation.ExtraInfo,
                          Deleted = comicsInformation.Deleted
                      };

        return exports
            .ToListAsync();
    }

    public Task<Story> GetInformationAsync(IdSearch model)
    {
        return _context.Stories
            .Include(s => s.Code)
            .Include(s => s.OriginStory)
            .Include(s => s.StoryArtist)
            .ThenInclude(sa => sa.Artist)
            .Include(s => s.StoryCharacter)
            .ThenInclude(sc => sc.Character)
            .Include(s => s.StoryBook)
            .ThenInclude(sb => sb.Book)
            .Include(s => s.StoryFromOrigin)
            .SingleOrDefaultAsync(s => s.Id == model.StoryId || s.Code.Id == model.CodeId);
    }
}
