using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch, ArtistEditModel>
    {
        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(artistsService, mapper)
        {
        }

        public void AddArtistStory(ObservableChangedCollection<StoryArtistEditModel> storyArtists, int? storyId)
        {
            Item.AddStoryArtist(storyArtists, storyId);
        }

        public ObservableChangedCollection<StoryArtistEditModel> GetStoryArtists()
        {
            return Item.GetStoryArtists();
        }

        public void AddMainArtist(ObservableChangedCollection<PseudonymEditModel> mainArtists, int? id)
        {
            Item.AddMainArtist(mainArtists, id);
        }

        public void AddPseudonymArtist(ObservableChangedCollection<PseudonymEditModel> pseudonymArtists, int? id)
        {
            Item.AddPseudonymArtist(pseudonymArtists, id);
        }

        public ObservableChangedCollection<PseudonymEditModel> GetMainArtists()
        {
            return Item.GetMainArtists();
        }

        public ObservableChangedCollection<PseudonymEditModel> GetPseudonymArtists()
        {
            return Item.GetPseudonymArtists();
        }
    }
}
