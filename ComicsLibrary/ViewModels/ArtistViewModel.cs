using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ComicsLibrary.EditModels;

namespace ComicsLibrary.ViewModels
{
    public class ArtistViewModel : BasicViewModel
    {
        protected readonly IArtistsService _artistsService;
        private ArtistEditModel _artist;

        private ICollection<ArtistOutputModel> _artists;
        private CollectionViewSource _artistsViewSource;
        private string _artistFilter;

        public ArtistViewModel(IArtistsService artistsService,
            IMapper mapper) : base(mapper)
        {
            _artistsService = artistsService;
        }

        private void FilterResults(object sender, FilterEventArgs e)
        {
            if (_artistFilter is null || _artistFilter.Length == 0)
            {
                e.Accepted = true;
                return;
            }

            if (e.Item is ArtistOutputModel artist)
            {
                e.Accepted = artist.Name.Contains(_artistFilter, System.StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private async void GetArtistsAsync()
        {
            _artists = await _artistsService.GetAsync(new ComicsStore.MiddleWare.Models.Search.BasicSearchModel { });
            _artistsViewSource = new CollectionViewSource
            {
                Source = _artists
            };
            _artistsViewSource.Filter += FilterResults;
            _artistsViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public string ArtistFilter
        {
            get
            {
                return _artistFilter;
            }
            set
            {
                _artistFilter = value;
                _artistsViewSource.View.Refresh();
            }
        }

        public ICollectionView Artists
        {
            get 
            {
                if (_artistsViewSource is null)
                {
                    GetArtistsAsync();
                }

                return _artistsViewSource.View;
            }
        }

        public async Task<bool> GetArtistAsync(int id, bool links = true, bool forceRefresh = false)
        {
            if (!forceRefresh && id == Artist.Id)
            {
                return true;
            }

            Artist = Mapper.Map<ArtistEditModel>(await _artistsService.GetAsync(id));

            if (Artist is not null)
            {
                Artist.StoryArtists.Clear();

                if (links)
                {
                    var storiesOutput = await _artistsService.GetStoriesAsync(id);
                    foreach (var s in storiesOutput)
                    {
                        Artist.StoryArtists.Add(Mapper.Map<StoryArtistEditModel>(s));
                    }
                }

                return true;
            }

            return false;
        }

        public ArtistEditModel Artist
        {
            get => _artist;
            private set
            {
                _artist = value;
            }
        }
    }
}
