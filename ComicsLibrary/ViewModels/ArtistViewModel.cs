using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Models.Input;
using System.Collections.Generic;
using System;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearchModel, ArtistEditModel>
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
