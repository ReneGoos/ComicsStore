using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services
{
    public class ArtistsService : ComicsStoreService<Artist, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch>, IArtistsService
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;

        public ArtistsService(IComicsStoreMainRepository<Artist, BasicSearch> artistsRepository,
            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository,
            IMapper mapper) : base(artistsRepository, mapper)
        {
            _storyArtistsRepository = storyArtistsRepository;
        }

        public async Task<ICollection<ArtistStoryOutputModel>> GetStoriesAsync(int artistId)
        {
            var storyArtists = await _storyArtistsRepository.GetAsync(null, artistId);

            try
            {
                return Mapper.Map<ICollection<ArtistStoryOutputModel>>(storyArtists);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
