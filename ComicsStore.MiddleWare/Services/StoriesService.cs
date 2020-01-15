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

namespace ComicsStore.MiddleWare.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly IComicsStoreCrossRepository<StoryArtist> _storyArtistsRepository;
        private readonly IComicsStoreCrossRepository<StoryBook> _storyBooksRepository;
        private readonly IComicsStoreCrossRepository<StoryCharacter> _storyCharactersRepository;
        private readonly IMapper _mapper;

        public StoriesService(IStoriesRepository storiesRepository,
            IComicsStoreCrossRepository<StoryArtist> storyArtistsRepository,
            IComicsStoreCrossRepository<StoryBook> storyBooksRepository,
            IComicsStoreCrossRepository<StoryCharacter> storyCharactersRepository,
            IMapper mapper)
        {
            _storiesRepository = storiesRepository;
            _storyArtistsRepository = storyArtistsRepository;
            _storyBooksRepository = storyBooksRepository;
            _storyCharactersRepository = storyCharactersRepository;
            _mapper = mapper;
        }

        public async Task<StoryOutputModel> AddAsync(StoryInputModel storyInput)
        {
            var story = _mapper.Map<Story>(storyInput);

            var storyResult = await _storiesRepository.AddAsync(story);

            return _mapper.Map<StoryOutputModel>(storyResult);
        }

        public async Task DeleteAsync(int id)
        {
            var story = await _storiesRepository.GetAsync(id);

            if (story == null)
            {
                return;
            }

            await _storiesRepository.DeleteAsync(story);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _storiesRepository.GetAsync(id) != null;
        }

        public async Task<List<StoryOutputModel>> GetAsync(StorySearchModel searchModel)
        {
            var stories = await _storiesRepository.GetAsync(searchModel);

            try
            {
                var storiesOutput = _mapper.Map<List<StoryOutputModel>>(stories);

                return storiesOutput;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StoryOutputModel> GetAsync(int id)
        {
            var story = await _storiesRepository.GetAsync(id);

            return _mapper.Map<StoryOutputModel>(story);
        }

        public async Task<List<StoryBookOutputModel>> GetBooksAsync(int storyId)
        {
            var storyBooks = await _storyBooksRepository.GetAsync(storyId, null);

            try
            {
                return _mapper.Map<List<StoryBookOutputModel>>(storyBooks);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<StoryCharacterOutputModel>> GetCharactersAsync(int storyId)
        {
            var storyCharacters = await _storyCharactersRepository.GetAsync(storyId, null);

            try
            {
                return _mapper.Map<List<StoryCharacterOutputModel>>(storyCharacters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StoryOutputModel> UpdateAsync(int id, StoryInputModel storyInput)
        {
            var story = _mapper.Map<Story>(storyInput);
            story.Id = id;

            story = await _storiesRepository.UpdateAsync(story);

            return _mapper.Map<StoryOutputModel>(story);
        }

        public async Task<StoryOutputModel> PatchAsync(int id, StoryInputPatchModel storyInput)
        {
            var modifiedData = JsonHelper.ModifiedData<StoryInputPatchModel, Story>(storyInput, _mapper);

            var story = await _storiesRepository.PatchAsync(id, modifiedData);

            return _mapper.Map<StoryOutputModel>(story);
        }
    }
}
