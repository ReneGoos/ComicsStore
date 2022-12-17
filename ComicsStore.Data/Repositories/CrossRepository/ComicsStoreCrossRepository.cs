using System.Threading.Tasks;
using ComicsStore.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;

namespace ComicsStore.Data.Repositories.CrossRepository
{
    public abstract class ComicsStoreCrossRepository<T, IObject> : ComicsStoreRepository<T>, IComicsStoreCrossRepository<T, IObject>
        where T : CrossTable
    {
        public ComicsStoreCrossRepository(ComicsStoreDbContext context) : base(context)
        {
        }

        protected async Task<T> UpdateItemAsync(DbSet<T> collection, T item, int id, int crossId)
        {
            var entity = await collection.FindAsync(id, crossId);
            if (entity == null)
            {
                return null;
            }

            item.CreationDate = entity.CreationDate;
            _context.Entry(entity).CurrentValues.SetValues(item);

            await SaveChangesAsync();

            return entity;
        }

        public abstract Task<List<T>> AddAsync(IEnumerable<T> value);
        public abstract Task DeleteAsync(IEnumerable<T> value);
        public abstract Task<List<T>> GetAsync(int? id, int? crossId);
        public abstract Task<List<T>> UpdateAsync(IEnumerable<T> value);
        public abstract void UpdateLinkedItems(IObject itemCurrent, IObject itemNew);
    }
}