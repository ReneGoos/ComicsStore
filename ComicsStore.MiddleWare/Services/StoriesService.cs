using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services;

public class StoriesService(IComicsStoreMainRepository<Story, StorySearch> storiesRepository,
    IComicsStoreCrossRepository<StoryArtist, IStoryArtist> storyArtistsRepository,
    IComicsStoreCrossRepository<StoryBook, IStoryBook> storyBooksRepository,
    IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> storyCharactersRepository,
    IMapper mapper) : ComicsStoreService<Story, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearch>(storiesRepository, mapper), IStoriesService
{
    private readonly IComicsStoreCrossRepository<StoryArtist, IStoryArtist> _storyArtistsRepository = storyArtistsRepository;
    private readonly IComicsStoreCrossRepository<StoryBook, IStoryBook> _storyBooksRepository = storyBooksRepository;
    private readonly IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter> _storyCharactersRepository = storyCharactersRepository;

    public async Task<ICollection<StoryArtistOutputModel>> GetArtistsAsync(int storyId)
    {
        var storyArtists = await _storyArtistsRepository.GetAsync(storyId, null);

        try
        {
            return Mapper.Map<ICollection<StoryArtistOutputModel>>(storyArtists);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<ICollection<StoryBookOutputModel>> GetBooksAsync(int storyId)
    {
        var storyBooks = await _storyBooksRepository.GetAsync(storyId, null);

        try
        {
            return Mapper.Map<ICollection<StoryBookOutputModel>>(storyBooks);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<ICollection<StoryCharacterOutputModel>> GetCharactersAsync(int storyId)
    {
        var storyCharacters = await _storyCharactersRepository.GetAsync(storyId, null);

        try
        {
            return Mapper.Map<ICollection<StoryCharacterOutputModel>>(storyCharacters);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
