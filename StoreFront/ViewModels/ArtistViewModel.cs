using AutoMapper;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using StoreFront.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StoreFront.ViewModels
{
    public class ArtistViewModel : InputViewModel
    {
        protected readonly IArtistsService artistsService;

        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(mapper)
        {
            this.artistsService = artistsService;
            Artist = new ArtistModel();
        }

        public async Task<bool> GetArtistAsync(int id, bool links = false)
        {
            var artistOutput = await artistsService.GetAsync(id);

            if (artistOutput is not null)
            {
                Artist = mapper.Map<ArtistModel>(artistOutput);
                return true;
            }

            return false;
        }
        public ArtistModel Artist { get; private set; }
    }
}
