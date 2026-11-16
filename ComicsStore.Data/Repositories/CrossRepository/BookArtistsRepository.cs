using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;

namespace ComicsStore.Data.Repositories.CrossRepository;

public class BookArtistsRepository(ComicsStoreDbContext context) : ComicsStoreCrossRepository<BookArtist, IBookArtist>(context), IComicsStoreCrossRepository<BookArtist, IBookArtist>
{
    public override Task<BookArtist> AddAsync(BookArtist value)
    {
        return AddItemAsync(_context.BookArtists, value);
    }

    public override Task<List<BookArtist>> AddAsync(IEnumerable<BookArtist> value)
    {
        throw new System.NotImplementedException();
    }

    public override Task DeleteAsync(BookArtist value)
    {
        return RemoveItemAsync(_context.BookArtists, value);
    }

    public override Task DeleteAsync(IEnumerable<BookArtist> value)
    {
        throw new System.NotImplementedException();
    }

    public override Task<List<BookArtist>> GetAsync()
    {
        return _context.BookArtists
            .ToListAsync();
    }

    public override Task<List<BookArtist>> GetAsync(int? id, int? crossId)
    {
        if (id == null && crossId == null)
        {
            return null;
        }

        return _context.BookArtists
            .Include(ba => ba.Artist)
            .Include(ba => ba.Book)
            .Where(b => id != null ? b.BookId == id : b.ArtistId == crossId)
            .ToListAsync();
    }

    public override Task<BookArtist> UpdateAsync(BookArtist value)
    {
        return UpdateItemAsync(_context.BookArtists, value, value.BookId, value.ArtistId);
    }

    public override Task<List<BookArtist>> UpdateAsync(IEnumerable<BookArtist> value)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateLinkedItems(IBookArtist itemCurrent, IBookArtist itemNew)
    {
        if (itemNew.BookArtist is not null)
        {
            // Delete children
            foreach (var existingChild in itemCurrent.BookArtist.ToList())
            {
                if (!itemNew.BookArtist.Any(c => c.ArtistId == existingChild.ArtistId && c.BookId == existingChild.BookId))
                {
                    _ = itemCurrent.BookArtist.Remove(existingChild);
                }
            }

            // Update and Insert children
            foreach (var childModel in itemNew.BookArtist.ToList())
            {
                var existingChild = itemCurrent.BookArtist
                    .SingleOrDefault(c => c.ArtistId == childModel.ArtistId && c.BookId == childModel.BookId && c.BookId != default && c.ArtistId != default);

                if (existingChild != null)
                {
                    if (!existingChild.ArtistType.Equals(childModel.ArtistType))
                    {
                        // Remove child
                        _ = itemCurrent.BookArtist.Remove(existingChild);

                        // Insert child
                        var newChild = new BookArtist
                        {
                            CreationDate = existingChild.CreationDate,
                            ArtistId = childModel.ArtistId,
                            BookId = childModel.BookId,
                            ArtistType = childModel.ArtistType
                        };
                        itemCurrent.BookArtist.Add(newChild);
                    }
                }
                else if (childModel.ArtistId > 0 && childModel.BookId > 0)
                {
                    // Insert child
                    var newChild = new BookArtist
                    {
                        ArtistId = childModel.ArtistId,
                        BookId = childModel.BookId,
                        ArtistType = childModel.ArtistType
                    };
                    itemCurrent.BookArtist.Add(newChild);
                }
            }
        }
    }
}