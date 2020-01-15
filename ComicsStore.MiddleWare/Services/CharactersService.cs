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

namespace ComicsStore.MiddleWare.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly IComicsStoreRepository<Character, BasicSearchModel> _charactersRepository;
        private readonly IMapper _mapper;

        public CharactersService(IComicsStoreRepository<Character, BasicSearchModel> charactersRepository,
            IMapper mapper)
        {
            _charactersRepository = charactersRepository;
            _mapper = mapper;
        }

        public async Task<CharacterOutputModel> AddAsync(CharacterInputModel characterInput)
        {
            var character = _mapper.Map<Character>(characterInput);

            var characterResult = await _charactersRepository.AddAsync(character);

            return _mapper.Map<CharacterOutputModel>(characterResult);
        }

        public async Task DeleteAsync(int id)
        {
            var character = await _charactersRepository.GetAsync(id);

            if (character == null)
            {
                return;
            }

            await _charactersRepository.DeleteAsync(character);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _charactersRepository.GetAsync(id) != null;
        }

        public async Task<List<CharacterOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var characters =  await _charactersRepository.GetAsync(searchModel);

            return _mapper.Map<List<CharacterOutputModel>>(characters);
        }

        public async Task<CharacterOutputModel> GetAsync(int id)
        {
            var character = await _charactersRepository.GetAsync(id);

            return _mapper.Map<CharacterOutputModel>(character);
        }

        public async Task<CharacterOutputModel> UpdateAsync(int id, CharacterInputModel characterInput)
        {
            var character = _mapper.Map<Character>(characterInput);
            character.Id = id;

            character = await _charactersRepository.UpdateAsync(character);

            return _mapper.Map<CharacterOutputModel>(character);
        }

        public async Task<CharacterOutputModel> PatchAsync(int id, CharacterInputModel characterInput)
        {
            var modifiedData = JsonHelper.ModifiedData<CharacterInputModel, Character>(characterInput, _mapper);

            var character = await _charactersRepository.PatchAsync(id, modifiedData);

            return _mapper.Map<CharacterOutputModel>(character);
        }
    }
}
