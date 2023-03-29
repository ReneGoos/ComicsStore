using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsLibrary.Core;
using System;
using System.Windows.Input;
using ComicsLibrary.Navigation;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch, ArtistEditModel>
    {
        private readonly IStoriesService _storiesService;

        public ICommand DeleteMainArtistFromListCommand { get; protected set; }
        public ICommand DeletePseudonymArtistFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public ArtistViewModel(IArtistsService artistsService,
            IStoriesService storiesService,
            INavigationService navigationService,
            IMapper mapper) : base(artistsService, navigationService, mapper)
        {
            DeleteMainArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteMainArtistFromList));
            DeletePseudonymArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeletePseudonymArtistFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));

            _storiesService = storiesService;
        }

        public async void HandleStory(int? storyId, int? oldStoryId)
        {
            var story = storyId.HasValue ? Mapper.Map<StoryOnlyEditModel>(await _storiesService.GetAsync(storyId.Value)) : null;
            Item.HandleStory(oldStoryId, story);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            Item.HandleStory(storyId, null);
        }

        public async void HandleMainArtist(int? mainArtistId, int? oldMainArtistId)
        {
            var artist = mainArtistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _itemService.GetAsync(mainArtistId.Value)) : null;
            Item.HandleMainArtist(oldMainArtistId, artist);
        }

        private void DeleteMainArtistFromList(int? mainArtistId)
        {
            Item.HandleMainArtist(mainArtistId, null);
        }

        public async void HandlePseudonymArtist(int? pseudonymArtistId, int? oldPseudonymArtistId)
        {
            var artist = pseudonymArtistId.HasValue ? Mapper.Map<ArtistOnlyEditModel>(await _storiesService.GetAsync(pseudonymArtistId.Value)) : null;
            Item.HandlePseudonymArtist(oldPseudonymArtistId, artist);
        }

        private void DeletePseudonymArtistFromList(int? pseudonymArtistId)
        {
            Item.HandlePseudonymArtist(pseudonymArtistId, null);
        }
    }
}
