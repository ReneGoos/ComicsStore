using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearch, ArtistEditModel>
    {
        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(artistsService, mapper)
        {
        }

        public void AddArtistStory(List<StoryArtistEditModel> storyArtists, int? storyId)
        {
            Item.AddStoryArtist(storyArtists, storyId);
        }

        public List<StoryArtistEditModel> GetStoryArtists()
        {
            return Item.GetStoryArtists();
        }

    }
}
