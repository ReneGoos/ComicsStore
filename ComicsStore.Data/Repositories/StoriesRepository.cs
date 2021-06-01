using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
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

        public override Task<Story> AddAsync(Story story)
        {
            return AddItemAsync(_context.Stories, story);
        }

        public override Task DeleteAsync(Story story)
        {
            return RemoveItemAsync(_context.Stories, story);
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
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower()))
                .Where(s => model.ExtraInfo == null || s.ExtraInfo.ToLower().Contains(model.ExtraInfo.ToLower()))
                .Where(s => !model.CodeId.HasValue || s.CodeId == model.CodeId)
                .Where(s => !model.StoryType.HasValue || s.StoryType == model.StoryType)
                .ToListAsync();

            return stories;
        }

        public override Task<Story> GetAsync(int storyId, bool extended = false)
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
                    .SingleOrDefaultAsync(s => s.Id == storyId);
            }

            return _context.Stories
                .Include(s => s.StoryArtist)
                .Include(s => s.StoryCharacter)
                .Include(s => s.StoryBook)
                .SingleOrDefaultAsync(s => s.Id == storyId);
        }

        public override Task<Story> UpdateAsync(Story story)
        {
            return UpdateItemAsync(_context.Stories, story, UpdateLinkedItems);
        }

        public override Task<Story> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Stories, id, data);
        }
    }
}