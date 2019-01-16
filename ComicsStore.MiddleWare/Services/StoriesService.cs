using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly IComicsStoreCrossRepository<StoryArtist> _storyArtistsRepository;
        private readonly IComicsStoreCrossRepository<StoryBook> _storyBooksRepository;
        private readonly IComicsStoreCrossRepository<StoryCharacter> _storyCharactersRepository;

        public StoriesService(IStoriesRepository storiesRepository,
            IComicsStoreCrossRepository<StoryArtist> storyArtistsRepository,
            IComicsStoreCrossRepository<StoryBook> storyBooksRepository,
            IComicsStoreCrossRepository<StoryCharacter> storyCharactersRepository
            )
        {
            _storiesRepository = storiesRepository;
            _storyArtistsRepository = storyArtistsRepository;
            _storyBooksRepository = storyBooksRepository;
            _storyCharactersRepository = storyCharactersRepository;
        }

        public async Task<StoryOutputModel> AddAsync(StoryInputModel storyInput)
        {
            var story = Mapper.Map<Story>(storyInput);

            var storyResult = await _storiesRepository.AddAsync(story);

            return Mapper.Map<StoryOutputModel>(storyResult);
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
                var storiesOutput = Mapper.Map<List<StoryOutputModel>>(stories);

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

            return Mapper.Map<StoryOutputModel>(story);
        }

        public async Task<List<StoryBookOutputModel>> GetBooksAsync(int storyId)
        {
            var storyBooks = await _storyBooksRepository.GetAsync(storyId, null);

            try
            {
                return Mapper.Map<List<StoryBookOutputModel>>(storyBooks);
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
                return Mapper.Map<List<StoryCharacterOutputModel>>(storyCharacters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<StoryOutputModel> UpdateAsync(int id, StoryInputModel storyInput)
        {
            var story = Mapper.Map<Story>(storyInput);
            story.Id = id;

            story = await _storiesRepository.UpdateAsync(story);

            return Mapper.Map<StoryOutputModel>(story);
        }

        public async Task<StoryOutputModel> PatchAsync(int id, StoryInputPatchModel storyInput)
        {
            var modifiedData = JsonHelper.ModifiedData<StoryInputPatchModel, Story>(storyInput);

            var story = await _storiesRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<StoryOutputModel>(story);
        }
    }
}
