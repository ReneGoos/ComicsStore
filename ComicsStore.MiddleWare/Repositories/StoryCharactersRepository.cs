using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class StoryCharactersRepository : ComicsStoreCrossRepository<StoryCharacter>,  IComicsStoreCrossRepository<StoryCharacter>
    {
        public StoryCharactersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<StoryCharacter> AddAsync(StoryCharacter storyCharacter)
        {
            return AddItemAsync(_context.StoryCharacters, storyCharacter);
        }

        public Task<List<StoryCharacter>> AddAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(StoryCharacter storyCharacter)
        {
            return RemoveItemAsync(_context.StoryCharacters, storyCharacter);
        }

        public Task DeleteAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryCharacter>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
            {
                return null;
            }

            return _context.StoryCharacters
                .Include(sc => sc.Character)
                .Include(sc => sc.Story)
                .ThenInclude(s => s.Code)
                .Include(sc => sc.Story)
                .ThenInclude(s => s.OriginStory)
                .Where(s => id != null ? s.StoryId == id.Value : s.CharacterId == crossId)
                .ToListAsync();
        }

        public Task<StoryCharacter> UpdateAsync(StoryCharacter storyCharacter)
        {
            return UpdateItemAsync(_context.StoryCharacters, storyCharacter, storyCharacter.StoryId, storyCharacter.CharacterId);
        }

        public Task<List<StoryCharacter>> UpdateAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }
    }
}