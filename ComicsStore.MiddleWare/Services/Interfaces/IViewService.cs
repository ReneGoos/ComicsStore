using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IViewService
    {
        Task<ICollection<ExportBooksOutputModel>> GetAsync(ViewSearch searchModel);
        Task<string> GetExportAsync(ViewSearch searchModel);
    }
}