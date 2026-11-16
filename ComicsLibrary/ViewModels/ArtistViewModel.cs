using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsLibrary.Core;
using System.Windows.Input;
using ComicsLibrary.Navigation;
using ComicsStore.Data.Common;
using SoftGoosR.Windows.Core;

namespace ComicsLibrary.ViewModels;

public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch, ArtistEditModel>
{
    private readonly IBooksService _booksService;
    private readonly IStoriesService _storiesService;

    public ICommand DeleteMainArtistFromListCommand { get; protected set; }
    public ICommand DeletePseudonymArtistFromListCommand { get; protected set; }
    public ICommand DeleteStoryFromListCommand { get; protected set; }
    public ICommand DeleteBookFromListCommand { get; protected set; }

    public ArtistViewModel(IArtistsService artistsService,
                           IBooksService booksService,
                           IStoriesService storiesService,
                           INavigationService navigationService,
                           IMapper mapper) : base(artistsService, navigationService, mapper)
    {
        DeleteMainArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteMainArtistFromList));
        DeletePseudonymArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeletePseudonymArtistFromList));
        DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
        _booksService = booksService;
        _storiesService = storiesService;
    }

    public async void HandleBook(int? bookId, int? oldBookId)
    {
        var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
        IsDirty |= Item.HandleBook(oldBookId, book, ItemPropertyChanged);
    }

    private void DeleteBookFromList(int? bookId)
    {
        IsDirty |= Item.HandleBook(bookId, null);
    }

    public async void HandleStory(int? storyId, int? oldStoryId)
    {
        var story = storyId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _storiesService.GetAsync(storyId.Value)) : null;
        IsDirty |= Item.HandleStory(oldStoryId, story, ItemPropertyChanged);
    }

    private void DeleteStoryFromList(int? storyId)
    {
        IsDirty |= Item.HandleStory(storyId, null);
    }

    public async void HandleMainArtist(int? mainArtistId, int? oldMainArtistId)
    {
        var artist = mainArtistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _itemService.GetAsync(mainArtistId.Value)) : null;
        IsDirty |= Item.HandleMainArtist(oldMainArtistId, artist, ItemPropertyChanged);
    }

    private void DeleteMainArtistFromList(int? mainArtistId)
    {
        IsDirty |= Item.HandleMainArtist(mainArtistId, null);
    }

    public async void HandlePseudonymArtist(int? pseudonymArtistId, int? oldPseudonymArtistId)
    {
        var artist = pseudonymArtistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _itemService.GetAsync(pseudonymArtistId.Value)) : null;
        IsDirty |= Item.HandlePseudonymArtist(oldPseudonymArtistId, artist, ItemPropertyChanged);
    }

    private void DeletePseudonymArtistFromList(int? pseudonymArtistId)
    {
        IsDirty |= Item.HandlePseudonymArtist(pseudonymArtistId, null);
    }

    public override void ItemChange(TableType table, int? id, ActionType actionType)
    {
        switch (actionType)
        {
            case ActionType.deleteItem:
                switch (table)
                {
                    case TableType.artist:
                        DeleteMainArtistFromList(id);
                        DeletePseudonymArtistFromList(id);
                        break;

                    case TableType.story:
                        DeleteStoryFromList(id);
                        break;
                }
                break;

            case ActionType.updateItem:
                switch (table)
                {
                    case TableType.artist:
                        HandleMainArtist(id, id);
                        HandlePseudonymArtist(id, id);
                        break;

                    case TableType.story:
                        HandleStory(id, id);
                        break;
                }
                break;
        }
    }
}
