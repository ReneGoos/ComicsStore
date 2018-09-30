﻿using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;

        public StoriesService(IStoriesRepository storiesRepository)
        {
            _storiesRepository = storiesRepository;
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

        public async Task<IEnumerable<StoryOutputModel>> GetAsync(StorySearchModel searchModel)
        {
            var stories =  await _storiesRepository.GetAsync(searchModel);

            return Mapper.Map<List<StoryOutputModel>>(stories);
        }

        public async Task<StoryOutputModel> GetAsync(int id)
        {
            var story = await _storiesRepository.GetAsync(id);

            return Mapper.Map<StoryOutputModel>(story);
        }

        public Task<string> GetExportAsync()
        {
            return _storiesRepository.GetExportAsync();
        }

        public async Task<StoryOutputModel> UpdateAsync(int id, StoryInputModel storyInput)
        {
            var story = Mapper.Map<Story>(storyInput);
            story.Id = id;

            story = await _storiesRepository.UpdateAsync(story);

            return Mapper.Map<StoryOutputModel>(story);
        }
    }
}