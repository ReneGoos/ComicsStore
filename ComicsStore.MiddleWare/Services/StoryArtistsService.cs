using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class StoryArtistsService : IStoryArtistsService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly IComicsStoreRepository<Artist, BasicSearchModel> _artistsRepository;
        private readonly IComicsStoreCrossRepository<StoryArtist> _storyArtistsRepository;
        private readonly IMapper _mapper;

        public StoryArtistsService(IStoriesRepository storiesRepository,
            IComicsStoreRepository<Artist, BasicSearchModel> artistsRepository,
            IComicsStoreCrossRepository<StoryArtist> storyArtistsRepository,
            IMapper mapper)
        {
            _storiesRepository = storiesRepository;
            _artistsRepository = artistsRepository;
            _storyArtistsRepository = storyArtistsRepository;
            _mapper = mapper;
        }

        public async Task<List<ArtistStoryOutputModel>> GetMainAsync(int artistId)
        {
            var storyArtists = await _storyArtistsRepository.GetAsync(null, artistId);

            try
            {
                return _mapper.Map<List<ArtistStoryOutputModel>>(storyArtists);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddSubAsync(int mainId, List<StoryArtistInputModel> input)
        {
            try
            {
                var storyArtists = _mapper.Map<List<StoryArtist>>(input);

                foreach (StoryArtist item in storyArtists)
                {
                    item.StoryId = mainId;
                    var bookSeriesResult = await _storyArtistsRepository.AddAsync(item);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<StoryArtistOutputModel>> GetSubAsync(int storyId)
        {
            var storyArtists = await _storyArtistsRepository.GetAsync(storyId, null);

            try
            {
                return _mapper.Map<List<StoryArtistOutputModel>>(storyArtists);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
