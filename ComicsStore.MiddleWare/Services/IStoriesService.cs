using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public interface IStoriesService : IComicsStoreService<StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearchModel>
    {
        Task<List<StoryCharacterOutputModel>> GetCharactersAsync(int storyId);
        Task<List<StoryBookOutputModel>> GetBooksAsync(int storyId);
    }
}
