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
            return UpdateItemAsync(_context.Stories, story, UpdateLinkedItems);
        }

        private bool UpdateLinkedItems(Story storyCurrent, Story storyNew)
        {
            if (storyNew.StoryCharacter is not null)
            {
                // Delete children
                foreach (var existingChild in storyCurrent.StoryCharacter)
                {
                    if (!storyNew.StoryCharacter.Any(c => c.CharacterId == existingChild.CharacterId))
                    {
                        _context.StoryCharacters.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in storyNew.StoryCharacter)
                {
                    var existingChild = storyCurrent.StoryCharacter
                        .Where(c => c.CharacterId == childModel.CharacterId && c.CharacterId != default)
                        .SingleOrDefault();

                    if (existingChild is null)
                    {
                        // Insert child
                        var newChild = new StoryCharacter
                        {
                            CharacterId = childModel.CharacterId,
                            StoryId = childModel.StoryId
                        };
                        storyCurrent.StoryCharacter.Add(newChild);
                    }
                }
            }

            if (storyNew.StoryArtist is not null)
            {
                // Delete children
                foreach (var existingChild in storyCurrent.StoryArtist)
                {
                    if (!storyNew.StoryArtist.Any(c => c.ArtistId == existingChild.ArtistId))
                    {
                        _context.StoryArtists.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in storyNew.StoryArtist)
                {
                    var existingChild = storyCurrent.StoryArtist
                        .Where(c => c.ArtistId == childModel.ArtistId && c.ArtistId != default)
                        .SingleOrDefault();

                    if (existingChild != null)
                    {
                        if (!existingChild.ArtistType.Equals(childModel.ArtistType))
                        {
                            // Remove child
                            _context.StoryArtists.Remove(existingChild);

                            // Insert child
                            var newChild = new StoryArtist
                            {
                                CreationDate = existingChild.CreationDate,
                                ArtistId = childModel.ArtistId,
                                StoryId = childModel.StoryId,
                                ArtistType = childModel.ArtistType
                            };
                            storyCurrent.StoryArtist.Add(newChild);
                        }
                    }
                    else
                    {
                        // Insert child
                        var newChild = new StoryArtist
                        {
                            ArtistId = childModel.ArtistId,
                            StoryId = childModel.StoryId
                        };
                        storyCurrent.StoryArtist.Add(newChild);
                    }
                }
            }

            if (storyNew.StoryBook is not null)
            {
                // Delete children
                foreach (var existingChild in storyCurrent.StoryBook)
                {
                    if (!storyNew.StoryBook.Any(c => c.BookId == existingChild.BookId))
                    {
                        _context.StoryBooks.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in storyNew.StoryBook)
                {
                    var existingChild = storyCurrent.StoryBook
                        .Where(c => c.BookId == childModel.BookId && c.BookId != default)
                        .SingleOrDefault();

                    if (existingChild is null)
                    {
                        // Insert child
                        var newChild = new StoryBook
                        {
                            BookId = childModel.BookId,
                            StoryId = childModel.StoryId
                        };
                        storyCurrent.StoryBook.Add(newChild);
                    }
                }
            }

            return true;
        }

        public Task<Story> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Stories, id, data);
        }
    }
}