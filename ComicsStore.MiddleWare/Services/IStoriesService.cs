using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public interface IStoriesService : IComicsStoreService<StoryInputModel, StoryOutputModel, StorySearchModel>
    {
        Task<string> GetExportAsync();
    }
}
