using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.MiddleWare.Repositories
{
    public abstract class ComicsStoreRepository<T> : IComicsStoreRepository<T>
        where T : BasicsTable
    {
        protected readonly ComicsStoreDbContext _context;

        public ComicsStoreRepository(ComicsStoreDbContext context)
        {
            _context = context;
        }

        protected async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowDataException(e);
            }
            catch (ArgumentException e)
            {
                ThrowDataException(e);
            }
        }

        private static void ThrowDataException(Exception e)
        {
            var errMsg = e.Message;

            if (e.InnerException != null)
            {
                errMsg = e.InnerException.Message;
            }

            throw new DataException(errMsg);
        }

        protected async Task<T> AddItemAsync(DbSet<T> collection, T item)
        {
            var entityEntry = await collection.AddAsync(item);

            await SaveChangesAsync();

            return entityEntry.Entity;
        }

        protected async Task RemoveItemAsync(DbSet<T> collection, T item)
        {
            collection.Remove(item);

            await SaveChangesAsync();
        }

        public abstract Task<T> AddAsync(T value);
        public abstract Task DeleteAsync(T value);
        public abstract Task<List<T>> GetAsync();
        public abstract Task<T> UpdateAsync(T value);
    }
}