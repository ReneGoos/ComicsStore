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
using ComicsStore.Data.Model;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch, ArtistEditModel>
    {
        public ICommand DeleteMainArtistFromListCommand { get; protected set; }
        public ICommand DeletePseudonymArtistFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public ArtistViewModel(IArtistsService artistsService,
            INavigationService navigationService,
            IMapper mapper) : base(artistsService, navigationService, mapper)
        {
            DeleteMainArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteMainArtistFromList));
            DeletePseudonymArtistFromListCommand = new RelayCommand<int?>(new Action<int?>(DeletePseudonymArtistFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        }

        public void AddArtistStory(int? storyId, int? oldStoryId)
        {
            Item.AddStoryArtist(storyId, oldStoryId);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            Item.AddStoryArtist(null, storyId);
        }

        public void AddMainArtist(int? mainArtistId, int? oldMainArtistId)
        {
            Item.AddMainArtist(mainArtistId, oldMainArtistId);
        }

        private void DeleteMainArtistFromList(int? mainArtistId)
        {
            Item.AddMainArtist(null, mainArtistId);
        }

        public void AddPseudonymArtist(int? pseudonymArtistId, int? oldPseudonymArtistId)
        {
            Item.AddPseudonymArtist(pseudonymArtistId, oldPseudonymArtistId);
        }

        private void DeletePseudonymArtistFromList(int? pseudonymArtistId)
        {
            Item.AddPseudonymArtist(null, pseudonymArtistId);
        }
    }
}
