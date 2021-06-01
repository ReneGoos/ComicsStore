using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.Data.Repositories.Interfaces
{
    public interface IExportBooksRepository
    {
        Task<List<ExportBook>> GetAsync(StorySeriesSearch model);
        Task<List<ExportStory>> GetStoryAsync(StorySeriesSearch model);
    }
}