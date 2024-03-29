﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.Data.Repositories.MainRepository
{
    public class StoriesRepository : ComicsStoreMainRepository<Story, StorySearch>, IComicsStoreMainRepository<Story, StorySearch>
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;
        private readonly IComicsStoreCrossRepository<StoryBook, IStoryBook> _storyBooksRepository;
        private readonly IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> _storyCharactersRepository;

        private bool UpdateLinkedItems(Story storyCurrent, Story storyNew)
        {
            _storyArtistsRepository.UpdateLinkedItems(storyCurrent, storyNew);
            _storyBooksRepository.UpdateLinkedItems(storyCurrent, storyNew);
            _storyCharactersRepository.UpdateLinkedItems(storyCurrent, storyNew);

            return true;
        }

        public StoriesRepository(ComicsStoreDbContext context,
            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository,
            IComicsStoreCrossRepository<StoryBook, IStoryBook> storyBooksRepository,
            IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> storyCharactersRepository)
            : base(context)
        {
            _storyArtistsRepository = storyArtistsRepository;
            _storyBooksRepository = storyBooksRepository;
            _storyCharactersRepository = storyCharactersRepository;
        }

        public override Task<Story> AddAsync(Story value)
        {
            return AddItemAsync(_context.Stories, value);
        }

        public override Task DeleteAsync(Story value)
        {
            return RemoveItemAsync(_context.Stories, value);
        }

        public override Task<List<Story>> GetAsync()
        {
            var stories = _context.Stories
                .ToListAsync();

            return stories;
        }

        public override Task<List<Story>> GetAsync(StorySearch model)
        {
            var stories = _context.Stories
                .Include(s => s.Code)
                .Include(s => s.OriginStory)
                .Include(s => s.StoryArtist)
                .Include(s => s.StoryCharacter)
                .Include(s => s.StoryBook)
                .Include(s => s.StoryFromOrigin)
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower()))
                .Where(s => model.ExtraInfo == null || s.ExtraInfo.ToLower().Contains(model.ExtraInfo.ToLower()))
                .Where(s => !model.CodeId.HasValue || s.CodeId == model.CodeId)
                .Where(s => !model.StoryType.HasValue || s.StoryType == model.StoryType)
                .ToListAsync();

            return stories;
        }

        public override Task<Story> GetAsync(int id, bool extended)
        {
            if (extended)
            {
                return _context.Stories
                    .Include(s => s.Code)
                    .Include(s => s.OriginStory)
                    .Include(s => s.StoryArtist)
                    .ThenInclude(sa => sa.Artist)
                    .Include(s => s.StoryCharacter)
                    .ThenInclude(sc => sc.Character)
                    .Include(s => s.StoryBook)
                    .ThenInclude(sb => sb.Book)
                    .Include(s => s.StoryFromOrigin)
                    .SingleOrDefaultAsync(s => s.Id == id);
            }

            return _context.Stories
                .Include(s => s.StoryArtist)
                .Include(s => s.StoryCharacter)
                .Include(s => s.StoryBook)
                .Include(s => s.StoryFromOrigin)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public override Task<Story> UpdateAsync(Story value)
        {
            return UpdateItemAsync(_context.Stories, value, UpdateLinkedItems);
        }

        public override Task<Story> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Stories, id, data);
        }
    }
}