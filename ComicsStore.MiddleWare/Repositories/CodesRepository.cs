using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class CodesRepository : ComicsStoreMainRepository<Code>, IComicsStoreRepository<Code, BasicSearchModel>
    {
        public CodesRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Code> AddAsync(Code code)
        {
            return AddItemAsync(_context.Codes, code);
        }

        public Task DeleteAsync(Code code)
        {
            return RemoveItemAsync(_context.Codes, code);
        }

        public Task<List<Code>> GetAsync(BasicSearchModel model)
        {
            var codes = _context.Codes
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return codes;
        }

        public Task<Code> GetAsync(int codeId)
        {
            return _context.Codes.FindAsync(codeId);
        }

        public Task<Code> UpdateAsync(Code code)
        {
            return UpdateItemAsync(_context.Codes, code);
        }

        public Task<Code> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Codes, id, data);
        }
    }
}