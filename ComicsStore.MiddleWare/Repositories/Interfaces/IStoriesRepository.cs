using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories.Interfaces
{
    public interface IStoriesRepository : IComicsStoreRepository<Story, StorySearchModel>
    {
    }
}