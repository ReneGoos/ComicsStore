using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class ArtistsRepository : ComicsStoreMainRepository<Artist>,  IComicsStoreRepository<Artist, BasicSearchModel>
    {
        public ArtistsRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public Task<Artist> AddAsync(Artist artist)
        {
            return AddItemAsync(_context.Artists, artist);
        }

        public Task DeleteAsync(Artist artist)
        {
            return RemoveItemAsync(_context.Artists, artist);
        }

        public Task<List<Artist>> GetAsync(BasicSearchModel model)
        {
            var artists = _context.Artists
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return artists;
        }

        public Task<Artist> GetAsync(int artistId)
        {
            return _context.Artists
                .Include(a => a.StoryArtist)
                .SingleOrDefaultAsync(a => a.Id == artistId);
        }

        public Task<Artist> UpdateAsync(Artist artist)
        {
            return UpdateItemAsync(_context.Artists, artist);
        }

        public Task<Artist> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Artists, id, data);
        }
    }
}