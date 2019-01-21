using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services
{
    public interface IExportBooksService
    {
        Task<List<ExportBooksOutputModel>> GetAsync(ExportBooksSearchModel searchModel);
        Task<string> GetExportAsync(ExportBooksSearchModel searchModel);
    }
}