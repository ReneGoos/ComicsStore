﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CharactersRepository : ComicsStoreMainRepository<Character, BasicSearchModel>, IComicsStoreMainRepository<Character, BasicSearchModel>
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

        public override Task<Character> AddAsync(Character character)
        {
            return AddItemAsync(_context.Characters, character);
        }

        public override Task DeleteAsync(Character character)
        {
            return RemoveItemAsync(_context.Characters, character);
        }

        public override Task<List<Character>> GetAsync()
        {
            var characters = _context.Characters
                .ToListAsync();

            return characters;
        }

        public override Task<List<Character>> GetAsync(BasicSearchModel model)
        {
            var characters = _context.Characters
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return characters;
        }

        public override Task<Character> GetAsync(int characterId, bool extended = false)
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

        public override Task<Character> UpdateAsync(Character character)
        {
            return UpdateItemAsync(_context.Characters, character, UpdateLinkedItems);
        }


        public override Task<Character> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Characters, id, data);
        }
    }
}