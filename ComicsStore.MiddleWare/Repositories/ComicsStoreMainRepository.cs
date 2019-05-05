using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.MiddleWare.Repositories
{
    public abstract class ComicsStoreMainRepository<T> : ComicsStoreRepository<T> where T : MainTable
    {
        public ComicsStoreMainRepository(ComicsStoreDbContext context) : base(context)
        {
        }

        public async Task<T> UpdateItemAsync(DbSet<T> collection, T item)
        {
            var entity = await collection.FindAsync(item.Id);
            if (entity == null)
            {
                return null;
            }

            item.CreationDate = entity.CreationDate;
            _context.Entry(entity).CurrentValues.SetValues(item);

            await SaveChangesAsync();

            return entity;
        }

        public async Task<T> PatchItemAsync(DbSet<T> collection, int id, IDictionary<string,object> data)
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