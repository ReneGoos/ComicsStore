using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Repositories.Interfaces
{
    public interface IStoriesRepository : IComicsStoreRepository<Story, StorySearchModel>
    {
    }
}