using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class PublishersRepository : ComicsStoreRepository, IComicsStoreRepository<Publisher, BasicSearchModel>
    {
        public PublishersRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            var publisherEntity = await _context.Publishers.AddAsync(publisher);

            await SaveChangesAsync();

            return publisherEntity.Entity;
        }

        public async Task DeleteAsync(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);

            await SaveChangesAsync();
        }

        public Task<List<Publisher>> GetAsync(BasicSearchModel model)
        {
            var publishers = _context.Publishers
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return publishers;
        }

        public Task<Publisher> GetAsync(int publisherId)
        {
            return _context.Publishers.SingleOrDefaultAsync(s => s.Id == publisherId);
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            var publisherEntity = _context.Publishers.Update(publisher);

            await SaveChangesAsync();

            return publisherEntity.Entity;
        }
    }
}