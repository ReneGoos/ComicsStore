using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services;

public class ArtistsService(IComicsStoreMainRepository<Artist, BasicSearch> artistsRepository,
                            IComicsStoreCrossRepository<BookArtist, IBookArtist> bookArtistsRepository,
                            IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository,
                            IMapper mapper) : ComicsStoreService<Artist, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch>(artistsRepository, mapper), IArtistsService
{
    private readonly IComicsStoreCrossRepository<BookArtist, IBookArtist> _bookArtistsRepository = bookArtistsRepository;
    private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository = storyArtistsRepository;
    public async Task<ICollection<ArtistBookOutputModel>> GetBooksAsync(int artistId)
    {
        var bookArtists = await _bookArtistsRepository.GetAsync(null, artistId);

        try
        {
            return Mapper.Map<ICollection<ArtistBookOutputModel>>(bookArtists);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<ICollection<ArtistStoryOutputModel>> GetStoriesAsync(int artistId)
    {
        var storyArtists = await _storyArtistsRepository.GetAsync(null, artistId);

        try
        {
            return Mapper.Map<ICollection<ArtistStoryOutputModel>>(storyArtists);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
