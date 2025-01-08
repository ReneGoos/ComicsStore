using ComicsStore.Data.Common;
using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        var exports = from comicsInformation in _context.StorySeries
                      where (!model.Active.HasValue || comicsInformation.Deleted == model.Active.Value) &&
                            (model.Filter == null ||
                            model.Filter.Length == 0 ||
                            comicsInformation.StoryName.Contains(model.Filter, StringComparison.CurrentCultureIgnoreCase) ||
                            comicsInformation.SeriesName.Contains(model.Filter, StringComparison.CurrentCultureIgnoreCase) ||
                            comicsInformation.CharacterName.Contains(model.Filter, StringComparison.CurrentCultureIgnoreCase) ||
                            comicsInformation.ArtistName.Contains(model.Filter, StringComparison.CurrentCultureIgnoreCase)) &&
                            (!model.ArtistId.HasValue || comicsInformation.ArtistId == model.ArtistId.Value) &&
                            (!model.BookId.HasValue || comicsInformation.BookId == model.BookId.Value) &&
                            (!model.CharacterId.HasValue || comicsInformation.CharacterId == model.CharacterId.Value) &&
                            (!model.CodeId.HasValue || comicsInformation.StoryCodeId == model.CodeId.Value || comicsInformation.SeriesCodeId == model.CodeId.Value) &&
                            (!model.PublisherId.HasValue || comicsInformation.PublisherId == model.PublisherId.Value) &&
                            (!model.SeriesId.HasValue || comicsInformation.SeriesId == model.SeriesId.Value) &&
                            (!model.StoryId.HasValue || comicsInformation.StoryId == model.StoryId.Value)
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
}
