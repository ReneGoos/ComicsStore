using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class StoriesService : ComicsStoreService<Story, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearchModel>, IStoriesService
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;
        private readonly IComicsStoreCrossRepository<StoryBook, IStoryBook> _storyBooksRepository;
        private readonly IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> _storyCharactersRepository;

        public StoriesService(IComicsStoreMainRepository<Story, StorySearchModel> storiesRepository,
            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository,
            IComicsStoreCrossRepository<StoryBook, IStoryBook> storyBooksRepository,
            IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> storyCharactersRepository,
            IMapper mapper) : base(storiesRepository, mapper)
        {
            _storyArtistsRepository = storyArtistsRepository;
            _storyBooksRepository = storyBooksRepository;
            _storyCharactersRepository = storyCharactersRepository;
        }

        public async Task<ICollection<StoryArtistOutputModel>> GetArtistsAsync(int storyId)
        {
            var storyArtists = await _storyArtistsRepository.GetAsync(storyId, null);

            try
            {
                return Mapper.Map<ICollection<StoryArtistOutputModel>>(storyArtists);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<StoryBookOutputModel>> GetBooksAsync(int storyId)
        {
            var storyBooks = await _storyBooksRepository.GetAsync(storyId, null);

            try
            {
                return Mapper.Map<ICollection<StoryBookOutputModel>>(storyBooks);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<StoryCharacterOutputModel>> GetCharactersAsync(int storyId)
        {
            var storyCharacters = await _storyCharactersRepository.GetAsync(storyId, null);

            try
            {
                return Mapper.Map<ICollection<StoryCharacterOutputModel>>(storyCharacters);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
