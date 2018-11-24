using System;
using System.Data;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.MiddleWare.Repositories
{
    public abstract class ComicsStoreCrossRepository<T> : ComicsStoreRepository<T> where T : CrossTable
    {
        public ComicsStoreCrossRepository(ComicsStoreDbContext context) : base(context)
        {
        }

        public async Task<T> UpdateItemAsync(DbSet<T> collection, T item, int id, int crossId)
        {
            var entity = await collection.FindAsync( id, crossId );
            if (entity == null)
            {
                return null;
            }

            item.CreationDate = entity.CreationDate;
            _context.Entry(entity).CurrentValues.SetValues(item);

            await SaveChangesAsync();

            return entity;
        }
    }
}