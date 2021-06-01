using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class ArtistsRepository : ComicsStoreMainRepository<Artist, BasicSearch>,  IComicsStoreMainRepository<Artist, BasicSearch>
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;

        public ArtistsRepository(ComicsStoreDbContext context,
            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository)
            : base(context)
        {
            _storyArtistsRepository = storyArtistsRepository;
        }

        public override Task<Artist> AddAsync(Artist artist)
        {
            return AddItemAsync(_context.Artists, artist);
        }

        public override Task DeleteAsync(Artist artist)
        {
            return RemoveItemAsync(_context.Artists, artist);
        }

        public override Task<List<Artist>> GetAsync()
        {
            var artists = _context.Artists
                .ToListAsync();

            return artists;
        }

        public override Task<List<Artist>> GetAsync(BasicSearch model)
        {
            var artists = _context.Artists
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return artists;
        }

        public override Task<Artist> GetAsync(int artistId, bool extended = false)
        {
            if (extended)
            {
                return _context.Artists
                    .Include(a => a.StoryArtist)
                    .ThenInclude(sa => sa.Story)
                    .SingleOrDefaultAsync(a => a.Id == artistId);
            }

            return _context.Artists
                .Include(a => a.StoryArtist)
                .SingleOrDefaultAsync(a => a.Id == artistId);
        }

        public override Task<Artist> UpdateAsync(Artist artist)
        {
            return UpdateItemAsync(_context.Artists, artist, UpdateLinkedItems);
        }

        private bool UpdateLinkedItems(Artist artistCurrent, Artist artistNew)
        {
            _storyArtistsRepository.UpdateLinkedItems(artistCurrent, artistNew);

            return true;
        }

        public override Task<Artist> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Artists, id, data);
        }
    }
}