using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public interface IExportBooksRepository
    {
        Task<List<ExportBook>> GetAsync(ExportBooksSearchModel model);
        Task<List<ExportStory>> GetStoryAsync(ExportBooksSearchModel model);
    }
}