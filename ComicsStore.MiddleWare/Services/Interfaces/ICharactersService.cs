using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ICharactersService : IComicsStoreService<CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearchModel>
    {
    }
}