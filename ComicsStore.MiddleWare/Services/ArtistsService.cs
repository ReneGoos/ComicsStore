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
    public class ArtistsService : IArtistsService
    {
        private readonly IComicsStoreRepository<Artist, BasicSearchModel> _artistsRepository;
        private readonly IComicsStoreCrossRepository<StoryArtist> _storyArtistsRepository;
        private readonly IMapper mapper;

        public ArtistsService(IComicsStoreRepository<Artist, BasicSearchModel> artistsRepository,
            IComicsStoreCrossRepository<StoryArtist> storyArtistsRepository,
            IMapper mapper)
        {
            _artistsRepository = artistsRepository;
            _storyArtistsRepository = storyArtistsRepository;
            this.mapper = mapper;
        }

        public async Task<ArtistOutputModel> AddAsync(ArtistInputModel artistInput)
        {
            var artist = mapper.Map<Artist>(artistInput);

            var artistResult = await _artistsRepository.AddAsync(artist);

            return mapper.Map<ArtistOutputModel>(artistResult);
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

        public async Task<List<ArtistOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var artists =  await _artistsRepository.GetAsync(searchModel);

            return mapper.Map<List<ArtistOutputModel>>(artists);
        }

        public async Task<ArtistOutputModel> GetAsync(int id)
        {
            var artist = await _artistsRepository.GetAsync(id);

            return mapper.Map<ArtistOutputModel>(artist);
        }

        public async Task<ArtistOutputModel> UpdateAsync(int id, ArtistInputModel artistInput)
        {
            var artist = mapper.Map<Artist>(artistInput);
            artist.Id = id;

            artist = await _artistsRepository.UpdateAsync(artist);

            return mapper.Map<ArtistOutputModel>(artist);
        }

        public async Task<ArtistOutputModel> PatchAsync(int id, ArtistInputModel artistInput)
        {
            var modifiedData = JsonHelper.ModifiedData<ArtistInputModel, Artist>(artistInput, mapper);

            var artist = await _artistsRepository.PatchAsync(id, modifiedData);

            return mapper.Map<ArtistOutputModel>(artist);
        }
    }
}
