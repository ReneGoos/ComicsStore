using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class ArtistsService : IComicsStoreService<ArtistInputModel, ArtistOutputModel, BasicSearchModel>
    {
        private readonly IComicsStoreRepository<Artist, BasicSearchModel> _artistsRepository;

        public ArtistsService(IComicsStoreRepository<Artist, BasicSearchModel> artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public async Task<ArtistOutputModel> AddAsync(ArtistInputModel artistInput)
        {
            var artist = Mapper.Map<Artist>(artistInput);

            var artistResult = await _artistsRepository.AddAsync(artist);

            return Mapper.Map<ArtistOutputModel>(artistResult);
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await _artistsRepository.GetAsync(id);

            if (artist == null)
            {
                return;
            }

            await _artistsRepository.DeleteAsync(artist);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _artistsRepository.GetAsync(id) != null;
        }

        public async Task<IEnumerable<ArtistOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var artists =  await _artistsRepository.GetAsync(searchModel);

            return Mapper.Map<List<ArtistOutputModel>>(artists);
        }

        public async Task<ArtistOutputModel> GetAsync(int id)
        {
            var artist = await _artistsRepository.GetAsync(id);

            return Mapper.Map<ArtistOutputModel>(artist);
        }

        public async Task<ArtistOutputModel> UpdateAsync(int id, ArtistInputModel artistInput)
        {
            var artist = Mapper.Map<Artist>(artistInput);
            artist.Id = id;

            artist = await _artistsRepository.UpdateAsync(artist);

            return Mapper.Map<ArtistOutputModel>(artist);
        }
    }
}
