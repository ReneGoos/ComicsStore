using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class StoryArtistsRepository : ComicsStoreCrossRepository<StoryArtist>,  IComicsStoreCrossRepository<StoryArtist>
    {
        public StoryArtistsRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<StoryArtist> AddAsync(StoryArtist storyArtist)
        {
            return AddItemAsync(_context.StoryArtists, storyArtist);
        }

        public Task<List<StoryArtist>> AddAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(StoryArtist storyArtist)
        {
            return RemoveItemAsync(_context.StoryArtists, storyArtist);
        }

        public Task DeleteAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryArtist>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
                return null;

            return _context.StoryArtists
                .Include(sa => sa.Artist)
                .Include(sa => sa.Story)
                .Where(s => id != null ? s.StoryId == id : s.ArtistId == crossId)
                .ToListAsync();
        }
        
        public Task<StoryArtist> UpdateAsync(StoryArtist storyArtist)
        {
            return UpdateItemAsync(_context.StoryArtists, storyArtist, storyArtist.StoryId, storyArtist.ArtistId);
        }

        public Task<List<StoryArtist>> UpdateAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }
    }
}