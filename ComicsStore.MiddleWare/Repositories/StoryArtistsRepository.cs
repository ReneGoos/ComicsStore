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

        public Task DeleteAsync(StoryArtist storyArtist)
        {
            return RemoveItemAsync(_context.StoryArtists, storyArtist);
        }
        
        public Task<StoryArtist> UpdateAsync(StoryArtist storyArtist)
        {
            return UpdateItemAsync(_context.StoryArtists, storyArtist, storyArtist.StoryId, storyArtist.ArtistId);
        }
    }
}