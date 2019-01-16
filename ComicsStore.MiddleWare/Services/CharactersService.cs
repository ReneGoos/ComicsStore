using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly IComicsStoreRepository<Character, BasicSearchModel> _charactersRepository;

        public CharactersService(IComicsStoreRepository<Character, BasicSearchModel> charactersRepository)
        {
            _charactersRepository = charactersRepository;
        }

        public async Task<CharacterOutputModel> AddAsync(CharacterInputModel characterInput)
        {
            var character = Mapper.Map<Character>(characterInput);

            var characterResult = await _charactersRepository.AddAsync(character);

            return Mapper.Map<CharacterOutputModel>(characterResult);
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

            return Mapper.Map<List<CharacterOutputModel>>(characters);
        }

        public async Task<CharacterOutputModel> GetAsync(int id)
        {
            var character = await _charactersRepository.GetAsync(id);

            return Mapper.Map<CharacterOutputModel>(character);
        }

        public async Task<CharacterOutputModel> UpdateAsync(int id, CharacterInputModel characterInput)
        {
            var character = Mapper.Map<Character>(characterInput);
            character.Id = id;

            character = await _charactersRepository.UpdateAsync(character);

            return Mapper.Map<CharacterOutputModel>(character);
        }

        public async Task<CharacterOutputModel> PatchAsync(int id, CharacterInputModel characterInput)
        {
            var modifiedData = JsonHelper.ModifiedData<CharacterInputModel, Character>(characterInput);

            var character = await _charactersRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<CharacterOutputModel>(character);
        }
    }
}
