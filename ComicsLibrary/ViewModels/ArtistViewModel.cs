using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Models;
using System.Threading.Tasks;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : InputViewModel
    {
        protected readonly IArtistsService _artistsService;
        private ArtistModel _artist;

        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(mapper)
        {
            _artistsService = artistsService;
            Artist = new ArtistModel
            {
                Stories = new ArtistStoryList()
            };

            Artist.Name = "Test";
            Artist.Stories.Add(new ArtistStoryModel
            {
                Name = "Fiets",
                Remark = "Test"
            });
        }

        public async Task<bool> GetArtistAsync(int id, bool links = false)
        {
            var artistOutput = await _artistsService.GetAsync(id);

            if (artistOutput is not null)
            {
                Artist = mapper.Map<ArtistModel>(artistOutput);
                Artist.Stories.Clear();

                var storiesOutput = await _artistsService.GetStoriesAsync(id);
                foreach (var s in storiesOutput)
                {
                    Artist.Stories.Add(mapper.Map<ArtistStoryModel>(s));
                }

                return true;
            }

            return false;
        }
        public ArtistModel Artist
        {
            get => _artist;
            private set
            {
                _artist = value;
                RaisePropertyChanged();
            }
        }
    }
}
