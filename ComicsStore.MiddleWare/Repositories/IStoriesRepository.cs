using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public interface IStoriesRepository : IComicsStoreRepository<Story, StorySearchModel>
    {
        Task<string> GetExportAsync();
    }
}