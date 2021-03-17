using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ICharactersService : IComicsStoreService<CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearchModel>
    {
        Task<ICollection<CharacterStoryOutputModel>> GetStoriesAsync(int characterId);
    }
}