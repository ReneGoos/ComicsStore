using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

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

        public Task DeleteAsync(StoryCharacter storyCharacter)
        {
            return RemoveItemAsync(_context.StoryCharacters, storyCharacter);
        }
        
        public Task<StoryCharacter> UpdateAsync(StoryCharacter storyCharacter)
        {
            return UpdateItemAsync(_context.StoryCharacters, storyCharacter, storyCharacter.StoryId, storyCharacter.CharacterId);
        }
    }
}