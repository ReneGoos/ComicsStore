using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CodesRepository : ComicsStoreRepository, IComicsStoreRepository<Code, BasicSearchModel>
    {
        public CodesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Code> AddAsync(Code code)
        {
            var codeEntity = await _context.Codes.AddAsync(code);

            await SaveChangesAsync();

            return codeEntity.Entity;
        }

        public async Task DeleteAsync(Code code)
        {
            _context.Codes.Remove(code);

            await SaveChangesAsync();
        }

        public Task<List<Code>> GetAsync(BasicSearchModel model)
        {
            var codes = _context.Codes
                .Where(s => model.Name == null || s.CodeName.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return codes;
        }

        public Task<Code> GetAsync(int codeId)
        {
            return _context.Codes.SingleOrDefaultAsync(s => s.Id == codeId);
        }

        public async Task<Code> UpdateAsync(Code code)
        {
            var codeEntity = _context.Codes.Update(code);

            await SaveChangesAsync();

            return codeEntity.Entity;
        }
    }
}