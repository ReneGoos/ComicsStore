using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class PublishersRepository : ComicsStoreMainRepository<Publisher>, IComicsStoreRepository<Publisher, BasicSearchModel>
    {
        public PublishersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Publisher> AddAsync(Publisher publisher)
        {
            return AddItemAsync(_context.Publishers, publisher);
        }

        public Task DeleteAsync(Publisher publisher)
        {
            return RemoveItemAsync(_context.Publishers, publisher);
        }

        public Task<List<Publisher>> GetAsync(BasicSearchModel model)
        {
            var publishers = _context.Publishers
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return publishers;
        }

        public Task<Publisher> GetAsync(int publisherId)
        {
            return _context.Publishers.FindAsync(publisherId);
        }

        public Task<Publisher> UpdateAsync(Publisher publisher)
        {
            return UpdateItemAsync(_context.Publishers, publisher);
        }

        public Task<Publisher> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Publishers, id, data);
        }
    }
}