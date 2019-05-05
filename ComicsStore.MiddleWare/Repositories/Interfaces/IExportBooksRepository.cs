using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories.Interfaces
{
    public interface IExportBooksRepository
    {
        Task<List<ExportBook>> GetAsync(StorySeriesSearchModel model);
        Task<List<ExportStory>> GetStoryAsync(StorySeriesSearchModel model);
    }
}