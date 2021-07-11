using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.Data.Repositories
{
    public abstract class ComicsStoreMainRepository<T, TSearch> : ComicsStoreRepository<T>
        where T : MainTable
        where TSearch : BasicSearch
    {
        public ComicsStoreMainRepository(ComicsStoreDbContext context) : base(context)
        {
        }

        public abstract Task<T> GetAsync(int id, bool extended = false);
        public abstract Task<List<T>> GetAsync(TSearch model);
        public abstract Task<T> PatchAsync(int id, IDictionary<string, object> data);

        public async Task<T> UpdateItemAsync(DbSet<T> collection, T item, Func<T, T, bool> updateLinkedItems = null)
        {
            var entity = await collection.FindAsync(item.Id);
            if (entity == null)
            {
                return null;
            }

            item.CreationDate = entity.CreationDate;
            _context.Entry(entity).CurrentValues.SetValues(item);

            if (updateLinkedItems is not null && !updateLinkedItems(entity, item))
            {
                return null;
            }

            await SaveChangesAsync();

            return entity;
        }

        public async Task<T> PatchItemAsync(DbSet<T> collection, int id, IDictionary<string, object> data)
        {
            var entity = await collection.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            data["DateUpdate"] = DateTime.Now;
            _context.Entry(entity).CurrentValues.SetValues(data);

            await SaveChangesAsync();

            return entity;
        }
    }
}