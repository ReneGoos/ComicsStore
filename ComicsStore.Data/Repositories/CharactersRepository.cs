using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class CharactersRepository : ComicsStoreMainRepository<Character, BasicSearch>, IComicsStoreMainRepository<Character, BasicSearch>
    {
        private readonly IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> _storyCharactersRepository;

        private bool UpdateLinkedItems(Character characterCurrent, Character characterNew)
        {
            _storyCharactersRepository.UpdateLinkedItems(characterCurrent, characterNew);

            return true;
        }

        public CharactersRepository(ComicsStoreDbContext context,
            IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> storyCharactersRepository)
            : base(context)
        {
            _storyCharactersRepository = storyCharactersRepository;
        }

        public override Task<Character> AddAsync(Character value)
        {
            return AddItemAsync(_context.Characters, value);
        }

        public override Task DeleteAsync(Character value)
        {
            return RemoveItemAsync(_context.Characters, value);
        }

        public override Task<List<Character>> GetAsync()
        {
            var characters = _context.Characters
                .ToListAsync();

            return characters;
        }

        public override Task<List<Character>> GetAsync(BasicSearch model)
        {
            var characters = _context.Characters
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return characters;
        }

        public override Task<Character> GetAsync(int characterId, bool extended = true)
        {
            if (extended)
            {
                return _context.Characters
                        .Include(c => c.StoryCharacter)
                        .ThenInclude(sc => sc.Story)
                        .SingleOrDefaultAsync(c => c.Id == characterId);
            }

            return _context.Characters
                    .Include(c => c.StoryCharacter)
                    .SingleOrDefaultAsync(c => c.Id == characterId);
        }

        public override Task<Character> UpdateAsync(Character value)
        {
            return UpdateItemAsync(_context.Characters, value, UpdateLinkedItems);
        }


        public override Task<Character> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Characters, id, data);
        }
    }
}