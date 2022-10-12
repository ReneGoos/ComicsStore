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

namespace ComicsLibrary.ViewModels
{
    public class StoryViewModel : BasicTableViewModel<IStoriesService, StoryInputModel, StoryInputPatchModel, StoryOutputModel, StorySearch, StoryEditModel>
    {
        public ICommand DeleteArtistFromListCommand { get; protected set; }
        public ICommand DeleteBookFromListCommand { get; protected set; }
        public ICommand DeleteCharacterFromListCommand { get; protected set; }
        public ICommand DeleteOriginFromListCommand { get; protected set; }

        private ICollection<StoryOutputModel> _originStories;

        private void StoryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Items" )
            {
                GetOriginStories();
            }
        }

        public StoryViewModel(IStoriesService storiesService,
            INavigationService navigationService,
            IMapper mapper) : base(storiesService, navigationService, mapper)
        {
            DeleteArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteArtistFromList));
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
            DeleteCharacterFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteCharacterFromList));
            DeleteOriginFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteOriginFromList));

            PropertyChanged += StoryViewModel_PropertyChanged;
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

        public void AddStoryArtist(int? artistId, int? oldArtistId)
        {
            Item.AddStoryArtist(artistId, oldArtistId);
        }

        public void DeleteArtistFromList(int? artistId)
        {
            Item.AddStoryArtist(null, artistId);
        }

        public void AddStoryBook(int? bookId, int? oldBookId)
        {
            Item.AddStoryBook(bookId, oldBookId);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.AddStoryBook(null, bookId);
        }

        public void AddStoryCharacter(int? characterId, int? oldCharacterId)
        {
            Item.AddStoryCharacter(characterId, oldCharacterId);
        }

        public void DeleteCharacterFromList(int? characterId)
        {
            Item.AddStoryCharacter(null, characterId);
        }

        public void AddStoryCode(int? codeId, int? oldCodeId)
        {
            Item.AddStoryCode(codeId, oldCodeId);
        }

        public void AddOriginStory(int? originStoryId, int? oldOriginStoryId)
        {
            Item.AddOriginStory(originStoryId, oldOriginStoryId);
        }

        public void AddStoryOrigin(int? originStoryId, int? oldOriginStoryId)
        {
            Item.AddStoryOrigin(originStoryId, oldOriginStoryId);
        }

        public void DeleteOriginFromList(int? originStoryId)
        {
            Item.AddStoryOrigin(null, originStoryId);
        }
    }
}
