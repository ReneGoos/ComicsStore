using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class StoryCharactersRepository : ComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>, IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>
    {
        public StoryCharactersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<StoryCharacter> AddAsync(StoryCharacter value)
        {
            return AddItemAsync(_context.StoryCharacters, value);
        }

        public override Task<List<StoryCharacter>> AddAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(StoryCharacter value)
        {
            return RemoveItemAsync(_context.StoryCharacters, value);
        }

        public override Task DeleteAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<StoryCharacter>> GetAsync()
        {
            return _context.StoryCharacters
                .ToListAsync();
        }

        public override Task<List<StoryCharacter>> GetAsync(int? id, int? crossId)
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

        public override Task<StoryCharacter> UpdateAsync(StoryCharacter value)
        {
            return UpdateItemAsync(_context.StoryCharacters, value, value.StoryId, value.CharacterId);
        }

        public override Task<List<StoryCharacter>> UpdateAsync(IEnumerable<StoryCharacter> value)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLinkedItems(IStoryCharacter itemCurrent, IStoryCharacter itemNew)
        {
            if (itemNew.StoryCharacter is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.StoryCharacter.ToList())
                {
                    if (!itemNew.StoryCharacter.Any(c => c.CharacterId == existingChild.CharacterId && c.StoryId == existingChild.StoryId))
                    {
                        _ = itemCurrent.StoryCharacter.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.StoryCharacter.ToList())
                {
                    var existingChild = itemCurrent.StoryCharacter
                        .SingleOrDefault(c => c.CharacterId == childModel.CharacterId && c.StoryId == childModel.StoryId && c.StoryId != default && c.CharacterId != default);

                    if (existingChild is null && childModel.CharacterId > 0 && childModel.StoryId > 0)
                    {
                        // Insert child
                        var newChild = new StoryCharacter
                        {
                            CharacterId = childModel.CharacterId,
                            StoryId = childModel.StoryId
                        };
                        itemCurrent.StoryCharacter.Add(newChild);
                    }
                }
            }
        }
    }
}