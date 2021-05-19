using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class CharactersService : ComicsStoreService<Character, CharacterInputModel, CharacterInputModel, CharacterOutputModel, BasicSearchModel>, ICharactersService
    {
        private readonly IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> _storyCharactersRepository;

        public CharactersService(IComicsStoreMainRepository<Character, BasicSearchModel> charactersRepository,
            IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> storyCharactersRepository,
            IMapper mapper) : base(charactersRepository, mapper)
        {
            _storyCharactersRepository = storyCharactersRepository;
        }

        public async Task<ICollection<CharacterStoryOutputModel>> GetStoriesAsync(int characterId)
        {
            var storyCharacters = await _storyCharactersRepository.GetAsync(null, characterId);

            try
            {
                return Mapper.Map<ICollection<CharacterStoryOutputModel>>(storyCharacters);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
