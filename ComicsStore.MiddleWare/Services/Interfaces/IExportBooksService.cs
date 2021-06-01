using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IExportBooksService
    {
        Task<ICollection<ExportBooksOutputModel>> GetAsync(StorySeriesSearch searchModel);
        Task<string> GetExportAsync(StorySeriesSearch searchModel);
    }
}