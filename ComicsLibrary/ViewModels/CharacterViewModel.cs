using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class CharacterViewModel : BasicViewModel
    {
        private readonly ICharactersService _charactersService;

        public CharacterViewModel(ICharactersService charactersService,
            IMapper mapper) : base(mapper)
        {
            _charactersService = charactersService;
        }
    }
}