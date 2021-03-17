using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        private ICollection<StoryArtistEditModel> _storyArtists;

        public ICollection<StoryArtistEditModel> StoryArtists
        {
            get => _storyArtists;
            set
            {
                _storyArtists = value;
                RaisePropertyChanged();
            }
        }
    }
}
