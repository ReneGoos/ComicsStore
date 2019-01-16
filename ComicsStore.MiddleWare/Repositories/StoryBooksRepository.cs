using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class StoryBooksRepository : ComicsStoreCrossRepository<StoryBook>,  IComicsStoreCrossRepository<StoryBook>
    {
        public StoryBooksRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<StoryBook> AddAsync(StoryBook storyBook)
        {
            return AddItemAsync(_context.StoryBooks, storyBook);
        }

        public Task<List<StoryBook>> AddAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(StoryBook storyBook)
        {
            return RemoveItemAsync(_context.StoryBooks, storyBook);
        }

        public Task DeleteAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryBook>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
                return null;

            return _context.StoryBooks
                .Include(sa => sa.BookId)
                .Where(s => id != null ? s.StoryId == id : s.BookId == crossId)
                .ToListAsync();
        }

        public Task<StoryBook> UpdateAsync(StoryBook storyBook)
        {
            return UpdateItemAsync(_context.StoryBooks, storyBook, storyBook.StoryId, storyBook.BookId);
        }

        public Task<List<StoryBook>> UpdateAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }
    }
}