using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class StoryArtistsService : IStoryArtistsService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly IComicsStoreRepository<Artist, BasicSearchModel> _artistsRepository;
        private readonly IComicsStoreCrossRepository<StoryArtist> _storyArtistsRepository;

        public StoryArtistsService(IStoriesRepository storiesRepository,
            IComicsStoreRepository<Artist, BasicSearchModel> artistsRepository,
            IComicsStoreCrossRepository<StoryArtist> storyArtistsRepository)
        {
            _storiesRepository = storiesRepository;
            _artistsRepository = artistsRepository;
            _storyArtistsRepository = storyArtistsRepository;
        }

        public async Task<List<ArtistStoryOutputModel>> GetMainAsync(int artistId)
        {
            var storyArtists = await _storyArtistsRepository.GetAsync(null, artistId);

            try
            {
                return Mapper.Map<List<ArtistStoryOutputModel>>(storyArtists);
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
                var storyArtists = Mapper.Map<List<StoryArtist>>(input);

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
                return Mapper.Map<List<StoryArtistOutputModel>>(storyArtists);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
