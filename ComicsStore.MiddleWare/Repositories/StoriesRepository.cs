using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class StoriesRepository : ComicsStoreMainRepository<Story>, IStoriesRepository
    {
        public StoriesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Story> AddAsync(Story story)
        {
            return AddItemAsync(_context.Stories, story);
        }

        public Task DeleteAsync(Story story)
        {
            return RemoveItemAsync(_context.Stories, story);
        }

        public Task<List<Story>> GetAsync(StorySearchModel model)
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

        public Task<Story> GetAsync(int storyId)
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

        public Task<Story> UpdateAsync(Story story)
        {
            return UpdateItemAsync(_context.Stories, story);
        }

        public Task<Story> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Stories, id, data);
        }
    }
}