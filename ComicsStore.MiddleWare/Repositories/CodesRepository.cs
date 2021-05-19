using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CodesRepository : ComicsStoreMainRepository<Code, BasicSearchModel>, IComicsStoreMainRepository<Code, BasicSearchModel>
    {
        public CodesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<Code> AddAsync(Code code)
        {
            return AddItemAsync(_context.Codes, code);
        }

        public override Task DeleteAsync(Code code)
        {
            return RemoveItemAsync(_context.Codes, code);
        }

        public override Task<List<Code>> GetAsync()
        {
            var codes = _context.Codes
                .ToListAsync();

            return codes;
        }

        public override Task<List<Code>> GetAsync(BasicSearchModel model)
        {
            var codes = _context.Codes
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return codes;
        }

        public override Task<Code> GetAsync(int codeId, bool extended = false)
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

        public override Task<Code> UpdateAsync(Code code)
        {
            return UpdateItemAsync(_context.Codes, code);
        }

        public override Task<Code> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Codes, id, data);
        }
    }
}