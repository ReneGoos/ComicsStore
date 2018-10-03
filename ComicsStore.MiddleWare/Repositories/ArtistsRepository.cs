using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public class ArtistsRepository : ComicsStoreRepository,  IComicsStoreRepository<Artist, BasicSearchModel>
    {
        public ArtistsRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public async Task<Artist> AddAsync(Artist artist)
        {
            var artistEntity = await _context.Artists.AddAsync(artist);

            await SaveChangesAsync();

            return artistEntity.Entity;
        }

        public async Task DeleteAsync(Artist artist)
        {
            _context.Artists.Remove(artist);

            await SaveChangesAsync();
        }

        public Task<List<Artist>> GetAsync(BasicSearchModel model)
        {
            var artists = _context.Artists
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return artists;
        }

        public Task<Artist> GetAsync(int artistId)
        {
            return _context.Artists.SingleOrDefaultAsync(s => s.Id == artistId);
        }

        public async Task<Artist> UpdateAsync(Artist artist)
        {
            var artistEntity = _context.Artists.Update(artist);

            await SaveChangesAsync();

            return artistEntity.Entity;
        }
    }
}