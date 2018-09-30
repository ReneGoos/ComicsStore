using System;
using System.Data;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ComicsStore.MiddleWare.Repositories
{
    public abstract class ComicsStoreRepository
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
    }
}