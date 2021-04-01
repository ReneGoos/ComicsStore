using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class CharacterViewModel : BasicTableViewModel<ICharactersService, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearchModel, CharacterEditModel>
    {
        public CharacterViewModel(ICharactersService charactersService,
            IMapper mapper) : base(charactersService, mapper)
        {
        }
    }
}