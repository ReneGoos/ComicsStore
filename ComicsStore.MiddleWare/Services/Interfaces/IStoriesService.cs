using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IStoriesService : IComicsStoreService<StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearch>
    {
        Task<ICollection<StoryCharacterOutputModel>> GetCharactersAsync(int storyId);
        Task<ICollection<StoryBookOutputModel>> GetBooksAsync(int storyId);
        Task<ICollection<StoryArtistOutputModel>> GetArtistsAsync(int storyId);
    }
}
