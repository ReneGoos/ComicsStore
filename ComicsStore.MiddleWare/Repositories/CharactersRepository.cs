﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CharactersRepository : ComicsStoreMainRepository<Character>, IComicsStoreRepository<Character, BasicSearchModel>
    {
        public CharactersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Character> AddAsync(Character character)
        {
            return AddItemAsync(_context.Characters, character);
        }

        public Task DeleteAsync(Character character)
        {
            return RemoveItemAsync(_context.Characters, character);
        }

        public Task<List<Character>> GetAsync(BasicSearchModel model)
        {
            var characters = _context.Characters
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return characters;
        }

        public Task<Character> GetAsync(int characterId)
        {
            return _context.Characters
                .Include(c => c.StoryCharacter)
                .SingleOrDefaultAsync(c => c.Id == characterId); ;
        }

        public Task<Character> UpdateAsync(Character character)
        {
            return UpdateItemAsync(_context.Characters, character);
        }

        public Task<Character> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Characters, id, data);
        }
    }
}