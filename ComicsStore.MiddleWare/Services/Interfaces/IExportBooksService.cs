using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IExportBooksService
    {
        Task<List<ExportBooksOutputModel>> GetAsync(StorySeriesSearchModel searchModel);
        Task<string> GetExportAsync(StorySeriesSearchModel searchModel);
    }
}