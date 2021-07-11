using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class StoryBooksRepository : ComicsStoreCrossRepository<StoryBook, IStoryBook>, IComicsStoreCrossRepository<StoryBook, IStoryBook>
    {
        public StoryBooksRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<StoryBook> AddAsync(StoryBook value)
        {
            return AddItemAsync(_context.StoryBooks, value);
        }

        public override Task<List<StoryBook>> AddAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(StoryBook value)
        {
            return RemoveItemAsync(_context.StoryBooks, value);
        }

        public override Task DeleteAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<StoryBook>> GetAsync()
        {
            return _context.StoryBooks
                .ToListAsync();
        }

        public override Task<List<StoryBook>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
            {
                return null;
            }

            return _context.StoryBooks
                .Include(sb => sb.Book)
                .Include(sb => sb.Story)
                .ThenInclude(s => s.Code)
                .Include(sb => sb.Story)
                .ThenInclude(s => s.OriginStory)
                .Where(s => id != null ? s.StoryId == id : s.BookId == crossId)
                .ToListAsync();
        }

        public override Task<StoryBook> UpdateAsync(StoryBook value)
        {
            return UpdateItemAsync(_context.StoryBooks, value, value.StoryId, value.BookId);
        }

        public override Task<List<StoryBook>> UpdateAsync(IEnumerable<StoryBook> value)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLinkedItems(IStoryBook itemCurrent, IStoryBook itemNew)
        {
            if (itemNew.StoryBook is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.StoryBook.ToList())
                {
                    if (!itemNew.StoryBook.Any(c => c.StoryId == existingChild.StoryId && c.BookId == existingChild.BookId))
                    {
                        _ = itemCurrent.StoryBook.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.StoryBook.ToList())
                {
                    var existingChild = itemCurrent.StoryBook
                            .SingleOrDefault(c => c.StoryId == childModel.StoryId && c.BookId == childModel.BookId && c.BookId != default && c.StoryId != default);

                    if (existingChild is null && childModel.BookId > 0 && childModel.StoryId > 0)
                    {
                        // Insert child
                        var newChild = new StoryBook
                        {
                            BookId = childModel.BookId,
                            StoryId = childModel.StoryId
                        };
                        itemCurrent.StoryBook.Add(newChild);
                    }
                }
            }
        }
    }
}