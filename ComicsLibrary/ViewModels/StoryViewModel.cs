using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.MiddleWare.Services;
using ComicsStore.Data.Model;

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : BasicTableViewModel<IStoriesService, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearch, StoryEditModel>
    {
        public ICommand DeleteArtistFromListCommand { get; protected set; }
        public ICommand DeleteBookFromListCommand { get; protected set; }
        public ICommand DeleteCharacterFromListCommand { get; protected set; }
        public ICommand DeleteOriginFromListCommand { get; protected set; }

        private ICollection<StoryOutputModel> _originStories;
        private readonly IArtistsService _artistsService;
        private readonly IBooksService _booksService;
        private readonly ICharactersService _charactersService;
        private readonly ICodesService _codesService;

        private void StoryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Items" )
            {
                GetOriginStories();
            }
        }

        public StoryViewModel(IStoriesService storiesService,
            IArtistsService artistsService,
            IBooksService booksService,
            ICharactersService charactersService, 
            ICodesService codesService,
            INavigationService navigationService,
            IMapper mapper) : base(storiesService, navigationService, mapper)
        {
            DeleteArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteArtistFromList));
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
            DeleteCharacterFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteCharacterFromList));
            DeleteOriginFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteOriginFromList));

            PropertyChanged += StoryViewModel_PropertyChanged;
            _artistsService = artistsService;
            _booksService = booksService;
            _charactersService = charactersService;
            _codesService = codesService;
        }

        public IEnumerable<StoryOutputModel> OriginStories
        {
            get
            {
                if (_originStories is null)
                {
                    GetOriginStories();
                }

                return _originStories;
            }
            private set
            {
                if (value is null)
                {
                    GetOriginStories();
                    RaisePropertyChanged();
                }
            }
        }

        private void GetOriginStories()
        {
            _originStories = _items.Where(item => item.OriginStoryId is null).OrderBy(item => item.Name).ToList();
            RaisePropertyChanged("OriginStories");
        }

        public async void HandleArtist(int? artistId, int? oldArtistId)
        {
            var artist = artistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _artistsService.GetAsync(artistId.Value)) : null;
            Item.HandleArtist(oldArtistId, artist);
        }

        public void DeleteArtistFromList(int? artistId)
        {
            Item.HandleArtist(artistId, null);
        }

        public async void HandleBook(int? bookId, int? oldBookId)
        {
            var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
            Item.HandleBook(oldBookId, book);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.HandleBook(bookId, null);
        }

        public async void HandleCharacter(int? characterId, int? oldCharacterId)
        {
            var character = characterId.HasValue ? Mapper.Map<CharacterOnlyEditModel>(await _charactersService.GetAsync(characterId.Value)) : null;
            Item.HandleCharacter(oldCharacterId, character);
        }

        public void DeleteCharacterFromList(int? characterId)
        {
            Item.HandleCharacter(characterId, null);
        }

        public void HandleCode(int? codeId, int? oldCodeId)
        {
            Item.HandleCode(codeId);
        }

        public async void HandleOriginStory(int? originStoryId, int? oldOriginStoryId)
        {
            var story = originStoryId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _itemService.GetAsync(originStoryId.Value)) : null;
            Item.HandleOriginStory(originStoryId, oldOriginStoryId);
        }

        public async void HandleStoryOrigin(int? originStoryId, int? oldOriginStoryId)
        {
            var originStory = originStoryId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _itemService.GetAsync(originStoryId.Value)) : null;
            Item.HandleStoryOrigin(oldOriginStoryId, originStory);
        }

        public void DeleteOriginFromList(int? originStoryId)
        {
            Item.HandleStoryOrigin(originStoryId, null);
        }
    }
}
