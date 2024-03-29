﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.Data.Repositories.MainRepository
{
    public class CodesRepository : ComicsStoreMainRepository<Code, BasicSearch>, IComicsStoreMainRepository<Code, BasicSearch>
    {
        public CodesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<Code> AddAsync(Code value)
        {
            return AddItemAsync(_context.Codes, value);
        }

        public override Task DeleteAsync(Code value)
        {
            return RemoveItemAsync(_context.Codes, value);
        }

        public override Task<List<Code>> GetAsync()
        {
            var codes = _context.Codes
                .ToListAsync();

            return codes;
        }

        public override Task<List<Code>> GetAsync(BasicSearch model)
        {
            var codes = _context.Codes
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return codes;
        }

        public override Task<Code> GetAsync(int codeId, bool extended)
        {
            //if (extended)
            {
                return _context.Codes
                    .Include(sc => sc.Series)
                    .Include(sc => sc.Story)
                    .SingleOrDefaultAsync(c => c.Id == codeId);
            }

            //return _context.Codes
            //    .SingleOrDefaultAsync(c => c.Id == codeId);
        }

        public override Task<Code> UpdateAsync(Code value)
        {
            return UpdateItemAsync(_context.Codes, value);
        }

        public override Task<Code> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Codes, id, data);
        }
    }
}