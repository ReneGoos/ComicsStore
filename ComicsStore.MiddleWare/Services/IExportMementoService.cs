using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services
{
    public interface IExportMementoService
    {
        Task<List<ExportMementoOutputModel>> GetAsync(ExportMementoSearchModel searchModel);
        Task<string> GetExportAsync();
    }
}