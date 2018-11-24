using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
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
                .Include(s => s.StoryArtist)
                .Include(s => s.StoryCharacter)
                .Include(s => s.StoryBook)
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return stories;
        }

        public Task<Story> GetAsync(int storyId)
        {
            return _context.Stories
                .Include(s => s.Code)
                .Include(s => s.StoryArtist)
                .ThenInclude(sa => sa.Artist)
                .Include(s => s.StoryCharacter)
                .ThenInclude(sc => sc.Character)
                .Include(s => s.StoryBook)
                .ThenInclude(sb => sb.Book)
                .SingleOrDefaultAsync(s => s.Id == storyId);
        }

        public Task<string> GetExportAsync()
        {
            /*
            var export = new StringBuilder();
            export.AppendLine("type: event-export");
            export.AppendLine("format: 1986.2");

            foreach (var story in await _context.Stories.Include(s => s.TimeSlots).Where(s => s.TimeSlots.Any(ts => ts.Day > DateTime.Today)).ToListAsync())
            {
                export.AppendLine("=");
                export.AppendLine($"name: {story.Name}");
                export.AppendLine($"title: {story.Title}");
                export.AppendLine($"description:\n{story.Description}");
                export.AppendLine("dates:");

                foreach (var timeSlot in story.TimeSlots.Where(ts => ts.Day > DateTime.Today))
                {
                    export.AppendLine($"\t{timeSlot.Day:dd-MM-yyyy HH:mm}");
                }

                if (story.RegularPrice > 0 || story.RegularDiscountPrice > 0)
                {
                    export.AppendLine("prices:");

                    if (story.RegularPrice > 0)
                    {
                        export.AppendLine($"\tregular: {story.RegularPrice:c}");
                    }

                    if (story.RegularDiscountPrice > 0)
                    {
                        export.AppendLine($"\tdiscount: {story.RegularDiscountPrice:c}");
                    }
                }
            }
            return export.ToString();
            */
            return null;
        }

        public Task<Story> UpdateAsync(Story story)
        {
            return UpdateItemAsync(_context.Stories, story);
        }
    }
}