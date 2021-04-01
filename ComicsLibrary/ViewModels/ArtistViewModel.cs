using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsStore.MiddleWare.Models.Output;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Models.Input;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicTableViewModel<IArtistsService, ArtistInputModel, ArtistInputModel, ArtistOutputModel, BasicSearchModel, ArtistEditModel>
    {
        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(artistsService, mapper)
        {
        }
    }
}
