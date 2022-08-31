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
    public class ArtistsRepository : ComicsStoreMainRepository<Artist, BasicSearch>, IComicsStoreMainRepository<Artist, BasicSearch>
    {
        private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository;

        public ArtistsRepository(ComicsStoreDbContext context,
            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository)
            : base(context)
        {
            _storyArtistsRepository = storyArtistsRepository;
        }

        public override Task<Artist> AddAsync(Artist value)
        {
            return AddItemAsync(_context.Artists, value);
        }

        public override Task DeleteAsync(Artist value)
        {
            return RemoveItemAsync(_context.Artists, value);
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
                    .Include(a => a.MainArtist)
                    .Include(a => a.PseudonymArtist)
                    .SingleOrDefaultAsync(a => a.Id == artistId);
            }

            var artists = _context.Artists
                .Include(a => a.StoryArtist)
                .Include(a => a.MainArtist)
                .Include(a => a.PseudonymArtist)
                .SingleOrDefaultAsync(a => a.Id == artistId);
            return artists;
        }

        public override Task<Artist> UpdateAsync(Artist value)
        {
            return UpdateItemAsync(_context.Artists, value, UpdateLinkedItems);
        }

        private void UpdateMainArtistLinkedItems(IMainArtist itemCurrent, IMainArtist itemNew)
        {
            if (itemNew.MainArtist is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.MainArtist.ToList())
                {
                    if (!itemNew.MainArtist.Any(c => c.MainArtistId == existingChild.MainArtistId && c.PseudonymArtistId == existingChild.PseudonymArtistId))
                    {
                        _ = itemCurrent.MainArtist.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.MainArtist.ToList())
                {
                    var existingChild = itemCurrent.MainArtist
                        .SingleOrDefault(c => c.MainArtistId == childModel.MainArtistId && c.PseudonymArtistId == childModel.PseudonymArtistId && c.PseudonymArtistId != default && c.MainArtistId != default);

                    if (existingChild is null)
                    {
                        if (childModel.MainArtistId > 0 && childModel.PseudonymArtistId > 0)
                        {
                            // Insert child
                            var newChild = new Pseudonym
                            {
                                MainArtistId = childModel.MainArtistId,
                                PseudonymArtistId = childModel.PseudonymArtistId
                            };
                            itemCurrent.MainArtist.Add(newChild);
                        }
                    }
                }
            }
        }

        private void UpdatePseudonymArtistLinkedItems(IPseudonymArtist itemCurrent, IPseudonymArtist itemNew)
        {
            if (itemNew.PseudonymArtist is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.PseudonymArtist.ToList())
                {
                    if (!itemNew.PseudonymArtist.Any(c => c.MainArtistId == existingChild.MainArtistId && c.PseudonymArtistId == existingChild.PseudonymArtistId))
                    {
                        _ = itemCurrent.PseudonymArtist.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.PseudonymArtist.ToList())
                {
                    var existingChild = itemCurrent.PseudonymArtist
                        .SingleOrDefault(c => c.MainArtistId == childModel.MainArtistId && c.PseudonymArtistId == childModel.PseudonymArtistId && c.PseudonymArtistId != default && c.MainArtistId != default);

                    if (existingChild is null)
                    {
                        if (childModel.MainArtistId > 0 && childModel.PseudonymArtistId > 0)
                        {
                            // Insert child
                            var newChild = new Pseudonym
                            {
                                MainArtistId = childModel.MainArtistId,
                                PseudonymArtistId = childModel.PseudonymArtistId
                            };
                            itemCurrent.PseudonymArtist.Add(newChild);
                        }
                    }
                }
            }
        }

        private bool UpdateLinkedItems(Artist artistCurrent, Artist artistNew)
        {
            _storyArtistsRepository.UpdateLinkedItems(artistCurrent, artistNew);
            UpdateMainArtistLinkedItems(artistCurrent, artistNew);
            UpdatePseudonymArtistLinkedItems(artistCurrent, artistNew);

            return true;
        }

        public override Task<Artist> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Artists, id, data);
        }
    }
}