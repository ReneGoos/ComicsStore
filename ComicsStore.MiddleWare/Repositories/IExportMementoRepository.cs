using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public interface IExportMementoRepository
    {
        Task<List<ExportMemento>> GetAsync(ExportMementoSearchModel model);
        Task<List<ExportStory>> GetStoryAsync(ExportMementoSearchModel model);
    }
}