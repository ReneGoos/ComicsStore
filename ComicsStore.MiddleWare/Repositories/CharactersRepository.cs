using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CharactersRepository : ComicsStoreRepository, IComicsStoreRepository<Character, BasicSearchModel>
    {
        public CharactersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Character> AddAsync(Character character)
        {
            var characterEntity = await _context.Characters.AddAsync(character);

            await SaveChangesAsync();

            return characterEntity.Entity;
        }

        public async Task DeleteAsync(Character character)
        {
            _context.Characters.Remove(character);

            await SaveChangesAsync();
        }

        public Task<List<Character>> GetAsync(BasicSearchModel model)
        {
            var characters = _context.Characters
                .Where(s => model.Name == null || s.CharacterName.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return characters;
        }

        public Task<Character> GetAsync(int characterId)
        {
            return _context.Characters.SingleOrDefaultAsync(s => s.Id == characterId);
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            var characterEntity = _context.Characters.Update(character);

            await SaveChangesAsync();

            return characterEntity.Entity;
        }
    }
}