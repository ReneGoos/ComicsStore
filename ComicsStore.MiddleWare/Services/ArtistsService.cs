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
using ComicsStore.MiddleWare.Repositories;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class ArtistsService : ComicsStoreService<Artist, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearchModel>, IArtistsService
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;

        public ArtistsService(IComicsStoreMainRepository<Artist, BasicSearchModel> artistsRepository,
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
