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
using ComicsStore.Data.Common;

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
                }
                else
                {
                    _originStories = value.ToList();
                }
                RaisePropertyChanged();
            }
        }

        private void GetOriginStories()
        {
            OriginStories = _items.Where(item => item.OriginStoryId is null).OrderBy(item => item.Name);
            //RaisePropertyChanged("OriginStories");
        }

        public async void HandleArtist(int? artistId, int? oldArtistId)
        {
            var artist = artistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _artistsService.GetAsync(artistId.Value)) : null;
            IsDirty = IsDirty || Item.HandleArtist(oldArtistId, artist);
        }

        public void DeleteArtistFromList(int? artistId)
        {
            IsDirty = IsDirty || Item.HandleArtist(artistId, null);
        }

        public async void HandleBook(int? bookId, int? oldBookId)
        {
            var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
            IsDirty = IsDirty || Item.HandleBook(oldBookId, book);
        }

        public void DeleteBookFromList(int? bookId)
        {
            IsDirty = IsDirty || Item.HandleBook(bookId, null);
        }

        public async void HandleCharacter(int? characterId, int? oldCharacterId)
        {
            var character = characterId.HasValue ? Mapper.Map<CharacterOnlyEditModel>(await _charactersService.GetAsync(characterId.Value)) : null;
            IsDirty = IsDirty || Item.HandleCharacter(oldCharacterId, character);
        }

        public void DeleteCharacterFromList(int? characterId)
        {
            IsDirty = IsDirty || Item.HandleCharacter(characterId, null);
        }

        public async void HandleCode(int? codeId, int? oldCodeId)
        {
            var code = codeId.HasValue ? Mapper.Map<CodeOnlyEditModel>(await _codesService.GetAsync(codeId.Value)) : null;
            IsDirty = IsDirty || Item.HandleCode(oldCodeId, code);
        }

        public void DeleteCode(int? codeId)
        {
            IsDirty = IsDirty || Item.HandleCode(codeId, null);
        }

        public async void HandleOriginStory(int? originStoryId, int? oldOriginStoryId)
        {
            var story = originStoryId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _itemService.GetAsync(originStoryId.Value)) : null;
            IsDirty = IsDirty || Item.HandleOriginStory(oldOriginStoryId, story);
        }

        public void DeleteOriginStory(int? originStoryId)
        {
            IsDirty = IsDirty || Item.HandleOriginStory(originStoryId, null);
        }

        public async void HandleStoryOrigin(int? originStoryId, int? oldOriginStoryId)
        {
            var originStory = originStoryId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _itemService.GetAsync(originStoryId.Value)) : null;
            IsDirty = IsDirty || Item.HandleStoryOrigin(oldOriginStoryId, originStory);
        }

        public void DeleteOriginFromList(int? originStoryId)
        {
            IsDirty = IsDirty || Item.HandleStoryOrigin(originStoryId, null);
        }

        public override async void ItemChange(TableType table, int? id, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.deleteItem:
                    switch (table)
                    {
                        case TableType.artist:
                            DeleteArtistFromList(id);
                            break;

                        case TableType.book:
                            DeleteBookFromList(id);
                            break;

                        case TableType.character:
                            DeleteCharacterFromList(id);
                            break;

                        case TableType.code:
                            DeleteCode(id);
                            break;

                        case TableType.story:
                            DeleteOriginStory(id);
                            DeleteOriginFromList(id);
                            UpdateItemsList(new StoryOutputModel { Id = id.Value }, true);
                            break;
                    }
                    break;

                case ActionType.updateItem:
                    switch (table)
                    {
                        case TableType.artist:
                            HandleArtist(id, id);
                            break;

                        case TableType.book:
                            HandleBook(id, id);
                            break;

                        case TableType.character:
                            HandleCharacter(id, id);
                            break;

                        case TableType.code:
                            HandleCode(id, id);
                            break;

                        case TableType.story:
                            HandleOriginStory(id, id);
                            HandleStoryOrigin(id, id);

                            var originStory = id.HasValue ? Mapper.Map<StoryOutputModel>(await _itemService.GetAsync(id.Value)) : null;
                            UpdateItemsList(originStory);
                            break;
                    }
                    break;
            }
        }
    }
}
