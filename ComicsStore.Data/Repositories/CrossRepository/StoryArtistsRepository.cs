﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;

namespace ComicsStore.Data.Repositories.CrossRepository
{
    public class StoryArtistsRepository : ComicsStoreCrossRepository<StoryArtist, IStoryArtist>, IComicsStoreCrossRepository<StoryArtist, IStoryArtist>
    {
        public StoryArtistsRepository(ComicsStoreDbContext context)
            : base(context)
        {
        }

        public override Task<StoryArtist> AddAsync(StoryArtist value)
        {
            return AddItemAsync(_context.StoryArtists, value);
        }

        public override Task<List<StoryArtist>> AddAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteAsync(StoryArtist value)
        {
            return RemoveItemAsync(_context.StoryArtists, value);
        }

        public override Task DeleteAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<StoryArtist>> GetAsync()
        {
            return _context.StoryArtists
                .ToListAsync();
        }

        public override Task<List<StoryArtist>> GetAsync(int? id, int? crossId)
        {
            if (id == null && crossId == null)
            {
                return null;
            }

            return _context.StoryArtists
                .Include(sa => sa.Artist)
                .Include(sa => sa.Story)
                .ThenInclude(s => s.Code)
                .Include(sa => sa.Story)
                .ThenInclude(s => s.OriginStory)
                .Where(s => id != null ? s.StoryId == id : s.ArtistId == crossId)
                .ToListAsync();
        }

        public override Task<StoryArtist> UpdateAsync(StoryArtist value)
        {
            return UpdateItemAsync(_context.StoryArtists, value, value.StoryId, value.ArtistId);
        }

        public override Task<List<StoryArtist>> UpdateAsync(IEnumerable<StoryArtist> value)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateLinkedItems(IStoryArtist itemCurrent, IStoryArtist itemNew)
        {
            if (itemNew.StoryArtist is not null)
            {
                // Delete children
                foreach (var existingChild in itemCurrent.StoryArtist.ToList())
                {
                    if (!itemNew.StoryArtist.Any(c => c.ArtistId == existingChild.ArtistId && c.StoryId == existingChild.StoryId))
                    {
                        _ = itemCurrent.StoryArtist.Remove(existingChild);
                    }
                }

                // Update and Insert children
                foreach (var childModel in itemNew.StoryArtist.ToList())
                {
                    var existingChild = itemCurrent.StoryArtist
                        .SingleOrDefault(c => c.ArtistId == childModel.ArtistId && c.StoryId == childModel.StoryId && c.StoryId != default && c.ArtistId != default);

                    if (existingChild != null)
                    {
                        if (!existingChild.ArtistType.Equals(childModel.ArtistType))
                        {
                            // Remove child
                            _ = itemCurrent.StoryArtist.Remove(existingChild);

                            // Insert child
                            var newChild = new StoryArtist
                            {
                                CreationDate = existingChild.CreationDate,
                                ArtistId = childModel.ArtistId,
                                StoryId = childModel.StoryId,
                                ArtistType = childModel.ArtistType
                            };
                            itemCurrent.StoryArtist.Add(newChild);
                        }
                    }
                    else if (childModel.ArtistId > 0 && childModel.StoryId > 0)
                    {
                        // Insert child
                        var newChild = new StoryArtist
                        {
                            ArtistId = childModel.ArtistId,
                            StoryId = childModel.StoryId,
                            ArtistType = childModel.ArtistType
                        };
                        itemCurrent.StoryArtist.Add(newChild);
                    }
                }
            }
        }
    }
}